using solarxr_protocol.data_feed;
using solarxr_protocol.data_feed.tracker;
using solarxr_protocol.datatypes;
using solarxr_protocol.datatypes.math;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YawPitchRollTable : MonoBehaviour
{
    public GameObject Networking;

    private const float SpacingX = 0.2f;
    private const float SpacingY = 0.1f;

    private static readonly Color GoodAngleDelta = new(0.0f, 0.7f, 0.0f);
    private static readonly Color BadAngleDelta = new(1.0f, 0.0f, 0.0f);
    private const int MaxAngleDelta = 5;

    private class TrackerRow
    {
        public TextMeshPro Name;
        public TextMeshPro Yaw;
        public TextMeshPro Pitch;
        public TextMeshPro Roll;
    }

    private readonly Dictionary<BodyPart, TrackerRow> trackerRows = new();

    void Start()
    {
        CreateRow("HEADER", "", "Yaw", "Pitch", "Roll", 0);

        trackerRows.Add(BodyPart.HEAD, CreateRow("HEAD", "Head", "", "", "", 1));
        trackerRows.Add(BodyPart.CHEST, CreateRow("CHEST", "Chest", "", "", "", 2));
        trackerRows.Add(BodyPart.HIP, CreateRow("HIP", "Hip", "", "", "", 3));
        trackerRows.Add(BodyPart.LEFT_UPPER_LEG, CreateRow("LEFT_UPPER_LEG", "L Thigh", "", "", "", 4));
        trackerRows.Add(BodyPart.RIGHT_UPPER_LEG, CreateRow("RIGHT_UPPER_LEG", "R Thigh", "", "", "", 5));
        trackerRows.Add(BodyPart.LEFT_LOWER_LEG, CreateRow("LEFT_LOWER_LEG", "L Ankle", "", "", "", 6));
        trackerRows.Add(BodyPart.RIGHT_LOWER_LEG, CreateRow("RIGHT_LOWER_LEG", "R Ankle", "", "", "", 7));
        trackerRows.Add(BodyPart.LEFT_FOOT, CreateRow("LEFT_FOOT", "L Foot", "", "", "", 8));
        trackerRows.Add(BodyPart.RIGHT_FOOT, CreateRow("RIGHT_FOOT", "R Foot", "", "", "", 9));
    }

    void Update()
    {
        var latestUpdate = Networking.GetComponent<NetworkHumanSkeleton>().LatestDataFeedUpdate;
        if (latestUpdate == null)
        {
            return;
        }

        var dataFeed = latestUpdate.Update;

        var headEulerAngles = Vector3.zero;
        var headTracker = FindTracker(dataFeed, BodyPart.HEAD);
        if (headTracker.HasValue)
        {
            var rotation = headTracker.Value.RotationReferenceAdjusted;
            if (rotation.HasValue)
            {
                headEulerAngles = RHSToLHSQuaternion(rotation.Value).eulerAngles;
            }
        }

        foreach (var (bodyPart, trackerRow) in trackerRows)
        {
            var updated = false;

            var tracker = FindTracker(dataFeed, bodyPart);
            if (tracker.HasValue)
            {
                var rotation = tracker.Value.RotationReferenceAdjusted;
                if (rotation.HasValue)
                {
                    var eulerAngles = RHSToLHSQuaternion(rotation.Value).eulerAngles;

                    // Yaw
                    var yaw = (int)(eulerAngles.y - headEulerAngles.y);
                    if (yaw < 0)
                    {
                        yaw += 360;
                    }
                    if (yaw >= 180)
                    {
                        yaw -= 360;
                    }

                    if (yaw < 0)
                    {
                        trackerRow.Yaw.text = $"\u2190 {-yaw}";
                    }
                    else if (yaw > 0)
                    {
                        trackerRow.Yaw.text = $"{yaw} \u2192";
                    }
                    else
                    {
                        trackerRow.Yaw.text = "\u2191";
                    }

                    trackerRow.Yaw.color = AngleDeltaColor(yaw);

                    // Pitch
                    var pitch = (int)eulerAngles.x;
                    if (pitch >= 180)
                    {
                        pitch -= 360;
                    }

                    if (pitch < 0)
                    {
                        trackerRow.Pitch.text = $"\u2196 {-pitch}";
                    }
                    else if (pitch > 0)
                    {
                        trackerRow.Pitch.text = $"{pitch} \u2197";
                    }
                    else
                    {
                        trackerRow.Roll.text = "\u2191";
                    }

                    trackerRow.Pitch.color = AngleDeltaColor(pitch);

                    // Roll
                    var roll = (int)eulerAngles.z;
                    if (roll >= 180)
                    {
                        roll -= 360;
                    }

                    if (roll < 0)
                    {
                        trackerRow.Roll.text = $"\u2196 {-roll}";
                    }
                    else if (roll > 0)
                    {
                        trackerRow.Roll.text = $"{roll} \u2197";
                    }
                    else
                    {
                        trackerRow.Roll.text = "\u2191";
                    }

                    trackerRow.Roll.color = AngleDeltaColor(roll);

                    updated = true;
                }
            }

            if (!updated)
            {
                trackerRow.Yaw.text = "-";
                trackerRow.Yaw.color = Color.gray;

                trackerRow.Pitch.text = "-";
                trackerRow.Pitch.color = Color.gray;

                trackerRow.Roll.text = "-";
                trackerRow.Roll.color = Color.gray;
            }
        }
    }

    private TrackerRow CreateRow(string key, string name, string yaw, string pitch, string roll, int row)
    {
        return new()
        {
            Name = CreateText($"{key}_NAME", name, row, 0),
            Yaw = CreateText($"{key}_YAW", yaw, row, 1),
            Pitch = CreateText($"{key}_PITCH", pitch, row, 2),
            Roll = CreateText($"{key}_ROLL", roll, row, 3),
        };
    }

    private TextMeshPro CreateText(string key, string text, int row, int column)
    {
        var gameObject = new GameObject();
        gameObject.name = key;
        gameObject.transform.parent = transform;

        var textTemplate = GetComponent<TextMeshPro>();

        var textComponent = gameObject.AddComponent<TextMeshPro>();
        textComponent.text = text;
        textComponent.font = textTemplate.font;
        textComponent.fontSize = textTemplate.fontSize;
        textComponent.fontStyle = textTemplate.fontStyle;
        textComponent.horizontalAlignment = textTemplate.horizontalAlignment;
        textComponent.verticalAlignment = textTemplate.verticalAlignment;
        textComponent.color = textTemplate.color;

        // Can only be positioned after adding TextMeshPro
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(SpacingX * column, -SpacingY * row, 0.0f);

        return textComponent;
    }

    private TrackerData? FindTracker(DataFeedUpdate dataFeed, BodyPart bodyPart)
    {
        for (var i = 0; i < dataFeed.DevicesLength; ++i)
        {
            var optionalDevice = dataFeed.Devices(i);
            if (optionalDevice.HasValue)
            {
                var device = optionalDevice.Value;
                for (var j = 0; j < device.TrackersLength; ++j)
                {
                    var optionalTracker = device.Trackers(j);
                    if (optionalTracker.HasValue)
                    {
                        var tracker = optionalTracker.Value;
                        if (tracker.Info.HasValue && tracker.Info.Value.BodyPart == bodyPart)
                        {
                            return tracker;
                        }
                    }
                }
            }
        }

        return null;
    }

    private static Quaternion RHSToLHSQuaternion(Quat q)
    {
        return new Quaternion(q.X, q.Y, -q.Z, -q.W);
    }

    private static Color AngleDeltaColor(int angle)
    {
        var t = (float)Math.Abs(angle) / MaxAngleDelta;
        return Color.Lerp(GoodAngleDelta, BadAngleDelta, t);
    }
}
