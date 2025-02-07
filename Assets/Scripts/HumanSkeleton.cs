using solarxr_protocol.datatypes;
using solarxr_protocol.datatypes.math;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public GameObject Networking;
    public bool HideHead = false;
    public GameObject AxisTemplate;
    public Material BoneMaterial;
    public Material PhysicalTrackerMaterial;
    public Material SyntheticTrackerMaterial;

    private const float BoneRadius = 0.025f;
    private const float TrackerWidth = 0.04f;
    private const float TrackerHeight = 0.06f;
    private const float TrackerThickness = 0.03f;

    private readonly Dictionary<BodyPart, GameObject> boneObjects = new();
    private readonly Dictionary<BodyPart, GameObject> physicalTrackerObjects = new();
    private readonly Dictionary<BodyPart, GameObject> syntheticTrackerObjects = new();

    void Update()
    {
        var skeleton = Networking.GetComponent<NetworkHumanSkeleton>();
        if (skeleton == null)
        {
            return;
        }

        if (skeleton.LatestDataFeedUpdate == null)
        {
            return;
        }

        var update = skeleton.LatestDataFeedUpdate.Update;

        var headPosition = Vector3.zero;
        var headInvRotation = Quaternion.identity;
        for (var i = 0; i < update.BonesLength; ++i)
        {
            var optionalBone = update.Bones(i);
            if (optionalBone.HasValue)
            {
                var bone = optionalBone.Value;
                if (bone.BodyPart == BodyPart.HEAD &&
                    bone.HeadPositionG.HasValue &&
                    bone.RotationG.HasValue)
                {
                    headPosition = RHSToLHSVector3(bone.HeadPositionG.Value);
                    headInvRotation = 
                        Quaternion.Inverse(
                            RHSToLHSQuaternion(bone.RotationG.Value) 
                            * Quaternion.AngleAxis(-90.0f, Vector3.right));
                }
            }
        }

        for (var i = 0; i < update.BonesLength; ++i)
        {
            var optionalBone = update.Bones(i);
            if (!optionalBone.HasValue)
            {
                continue;
            }

            var bone = optionalBone.Value;

            if (bone.BodyPart == BodyPart.NONE ||
                (HideHead && bone.BodyPart == BodyPart.HEAD))
            {
                continue;
            }


            var bodyPart = bone.BodyPart.ToString();
            if (bodyPart.EndsWith("METACARPAL") ||
                bodyPart.EndsWith("PROXIMAL") ||
                bodyPart.EndsWith("DISTAL") ||
                bodyPart.EndsWith("INTERMEDIATE"))
            {
                continue;
            }

            if (!boneObjects.TryGetValue(bone.BodyPart, out var boneObject))
            {
                boneObject = new GameObject();
                boneObject.name = "BONE_" + bone.BodyPart.ToString();
                boneObject.transform.parent = transform;

                var cylinderObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                cylinderObject.transform.parent = boneObject.transform;
                cylinderObject.GetComponent<Renderer>().material = BoneMaterial;

                if (AxisTemplate != null)
                {
                    var axisObject = Instantiate(AxisTemplate);
                    axisObject.name = "Axis";
                    axisObject.transform.parent = boneObject.transform;
                    axisObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                    axisObject.SetActive(true);
                }

                boneObjects.Add(bone.BodyPart, boneObject);
            }

            if (bone.HeadPositionG.HasValue && bone.RotationG.HasValue)
            {
                boneObject.transform.SetLocalPositionAndRotation(
                    RHSToLHSVector3(bone.HeadPositionG.Value)
                        - (bone.BoneLength * 0.5f) * (RHSToLHSQuaternion(bone.RotationG.Value) * Vector3.up)
                        - headPosition,
                    RHSToLHSQuaternion(bone.RotationG.Value));

                var cylinderTransform = boneObject.transform.GetChild(0);
                cylinderTransform.localScale = new Vector3(BoneRadius, bone.BoneLength * 0.5f, BoneRadius);

                boneObject.SetActive(true);
            }
            else
            {
                boneObject.SetActive(false);
            }
        }

        for (int i = 0; i < update.DevicesLength; i++)
        {
            var optionalDevice = update.Devices(i);
            if (!optionalDevice.HasValue)
            {
                continue;
            }

            var device = optionalDevice.Value;
            for (int j = 0; j < device.TrackersLength; ++j)
            {
                var optionalTracker = device.Trackers(j);
                if (!optionalTracker.HasValue)
                {
                    continue;
                }

                var tracker = optionalTracker.Value;
                if (tracker.Status != TrackerStatus.OK || !tracker.Info.HasValue)
                {
                    continue;
                }

                var trackerBodyPart = tracker.Info.Value.BodyPart;
                if (trackerBodyPart == BodyPart.NONE ||
                    (HideHead && trackerBodyPart == BodyPart.HEAD))
                {
                    continue;
                }

                if (!physicalTrackerObjects.TryGetValue(trackerBodyPart, out var trackerObject))
                {
                    trackerObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    trackerObject.name = "PHYSICAL_TRACKER_" + trackerBodyPart.ToString();
                    trackerObject.transform.parent = transform;
                    trackerObject.transform.localScale = new Vector3(TrackerWidth, TrackerHeight, TrackerThickness);
                    trackerObject.GetComponent<Renderer>().material = PhysicalTrackerMaterial;

                    physicalTrackerObjects.Add(trackerBodyPart, trackerObject);
                }

                if (tracker.RotationReferenceAdjusted.HasValue &&
                    boneObjects.TryGetValue(trackerBodyPart, out var boneObject))
                {
                    trackerObject.transform.SetLocalPositionAndRotation(
                        // Attach to center of the bone
                        boneObject.transform.localPosition,
                        RHSToLHSQuaternion(tracker.RotationReferenceAdjusted.Value));
                    trackerObject.SetActive(true);
                }
                else
                {
                    trackerObject.SetActive(false);
                }
            }
        }

        for (int i = 0; i < update.SyntheticTrackersLength; i++)
        {
            var optionalTracker = update.SyntheticTrackers(i);
            if (!optionalTracker.HasValue)
            {
                continue;
            }

            var tracker = optionalTracker.Value;
            if (tracker.Status != TrackerStatus.OK || !tracker.Info.HasValue)
            {
                continue;
            }

            var trackerBodyPart = tracker.Info.Value.BodyPart;
            if (trackerBodyPart == BodyPart.NONE ||
                (HideHead && trackerBodyPart == BodyPart.HEAD))
            {
                continue;
            }

            if (!syntheticTrackerObjects.TryGetValue(trackerBodyPart, out var trackerObject))
            {
                trackerObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                trackerObject.name = "SYNTHETIC_TRACKER_" + trackerBodyPart.ToString();
                trackerObject.transform.parent = transform;
                trackerObject.transform.localScale = new Vector3(TrackerWidth, TrackerHeight, TrackerThickness);
                trackerObject.GetComponent<Renderer>().material = SyntheticTrackerMaterial;

                syntheticTrackerObjects.Add(trackerBodyPart, trackerObject);
            }

            if (tracker.Position.HasValue && tracker.RotationReferenceAdjusted.HasValue)
            {
                trackerObject.transform.SetLocalPositionAndRotation(
                    RHSToLHSVector3(tracker.Position.Value) - headPosition,
                    RHSToLHSQuaternion(tracker.RotationReferenceAdjusted.Value));
                trackerObject.SetActive(true);
            }
            else
            {
                trackerObject.SetActive(false);
            }
        }

        transform.SetLocalPositionAndRotation(
            skeleton.LatestDataFeedUpdate.CameraPosition,
            skeleton.LatestDataFeedUpdate.CameraRotation * headInvRotation);
    }

    private static Vector3 RHSToLHSVector3(Vec3f v)
    {
        return new Vector3(v.X, v.Y, -v.Z);
    }

    private static Quaternion RHSToLHSQuaternion(Quat q)
    {
        return new Quaternion(q.X, q.Y, -q.Z, -q.W);
    }
}
