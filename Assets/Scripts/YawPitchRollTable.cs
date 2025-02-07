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
    public bool RawRotations;

    private const float SpacingX = 0.12f;
    private const float SpacingY = 0.05f;
    private const float ArrowOffsetX = 0.06f;

    private static readonly Color GoodAngleDelta = new(0.0f, 0.7f, 0.0f);
    private static readonly Color BadAngleDelta = new(1.0f, 0.0f, 0.0f);
    private const int MaxAngleDelta = 5;

    private class TrackerRow
    {
        public TextMeshPro Name;
        
        public TextMeshPro Yaw;
        public TextMeshPro YawLeft;
        public TextMeshPro YawRight;
        
        public TextMeshPro Pitch;
        public TextMeshPro PitchBack;
        public TextMeshPro PitchForward;
        
        public TextMeshPro Roll;
        public TextMeshPro RollLeft;
        public TextMeshPro RollRight;
    }

    private readonly Dictionary<BodyPart, TrackerRow> trackerRows = new();

    void Start()
    {
        CreateRow("HEADER", RawRotations ? "Raw" : "Adjusted", "Yaw", "Pitch", "Roll", 0, false);

        trackerRows.Add(BodyPart.HEAD, CreateRow("HEAD", "Head", "", "", "", 1, true));
        trackerRows.Add(BodyPart.CHEST, CreateRow("CHEST", "Chest", "", "", "", 2, true));
        trackerRows.Add(BodyPart.HIP, CreateRow("HIP", "Hip", "", "", "", 3, true));
        trackerRows.Add(BodyPart.LEFT_UPPER_LEG, CreateRow("LEFT_UPPER_LEG", "L Thigh", "", "", "", 4, true));
        trackerRows.Add(BodyPart.RIGHT_UPPER_LEG, CreateRow("RIGHT_UPPER_LEG", "R Thigh", "", "", "", 5, true));
        trackerRows.Add(BodyPart.LEFT_LOWER_LEG, CreateRow("LEFT_LOWER_LEG", "L Ankle", "", "", "", 6, true));
        trackerRows.Add(BodyPart.RIGHT_LOWER_LEG, CreateRow("RIGHT_LOWER_LEG", "R Ankle", "", "", "", 7, true));
        trackerRows.Add(BodyPart.LEFT_FOOT, CreateRow("LEFT_FOOT", "L Foot", "", "", "", 8, true));
        trackerRows.Add(BodyPart.RIGHT_FOOT, CreateRow("RIGHT_FOOT", "R Foot", "", "", "", 9, true));
    }

    void Update()
    {
        var latestUpdate = Networking.GetComponent<NetworkHumanSkeleton>().LatestDataFeedUpdate;
        if (latestUpdate == null)
        {
            return;
        }

        var dataFeed = latestUpdate.Update;

        var rootEulerAngles = Vector3.zero;
        var rootTracker = FindTracker(dataFeed, BodyPart.CHEST);
        if (rootTracker.HasValue)
        {
            var rotation = rootTracker.Value.RotationReferenceAdjusted;
            if (rotation.HasValue)
            {
                rootEulerAngles = RHSToLHSQuaternion(rotation.Value).eulerAngles;
            }
        }

        foreach (var (bodyPart, trackerRow) in trackerRows)
        {
            // Reset
            trackerRow.Yaw.text = "-";
            trackerRow.Yaw.color = Color.gray;
            trackerRow.YawLeft.color = Color.clear;
            trackerRow.YawRight.color = Color.clear;

            trackerRow.Pitch.text = "-";
            trackerRow.Pitch.color = Color.gray;
            trackerRow.PitchBack.color = Color.clear;
            trackerRow.PitchForward.color = Color.clear;

            trackerRow.Roll.text = "-";
            trackerRow.Roll.color = Color.gray;
            trackerRow.RollLeft.color = Color.clear;
            trackerRow.RollRight.color = Color.clear;

            var tracker = FindTracker(dataFeed, bodyPart);
            if (tracker.HasValue)
            {
                Quat? rotation;
                if (RawRotations)
                {
                    rotation = tracker.Value.Rotation;
                }
                else
                {
                    rotation = tracker.Value.RotationReferenceAdjusted;
                }

                if (rotation.HasValue)
                {
                    var eulerAngles = RHSToLHSQuaternion(rotation.Value).eulerAngles;

                    // Yaw
                    var yaw = (int)(eulerAngles.y - rootEulerAngles.y);
                    if (yaw < 0)
                    {
                        yaw += 360;
                    }
                    if (yaw >= 180)
                    {
                        yaw -= 360;
                    }

                    var yawColor = AngleDeltaColor(yaw);
                    if (yaw < 0)
                    {
                        trackerRow.Yaw.text = (-yaw).ToString();
                        trackerRow.Yaw.color = yawColor;
                        trackerRow.YawLeft.color = yawColor;
                    }
                    else if (yaw > 0)
                    {
                        trackerRow.Yaw.text = yaw.ToString();
                        trackerRow.Yaw.color = yawColor;
                        trackerRow.YawRight.color = yawColor;
                    }
                    else
                    {
                        trackerRow.Yaw.text = "\u25EF";
                        trackerRow.Yaw.color = yawColor;
                    }

                    // Pitch
                    var pitch = (int)eulerAngles.x;
                    if (pitch >= 180)
                    {
                        pitch -= 360;
                    }

                    var pitchColor = AngleDeltaColor(pitch);
                    if (pitch < 0)
                    {
                        trackerRow.Pitch.text = (-pitch).ToString();
                        trackerRow.Pitch.color = pitchColor;
                        trackerRow.PitchBack.color = pitchColor;
                    }
                    else if (pitch > 0)
                    {
                        trackerRow.Pitch.text = pitch.ToString();
                        trackerRow.Pitch.color = pitchColor;
                        trackerRow.PitchForward.color = pitchColor;
                    }
                    else
                    {
                        trackerRow.Roll.text = "\u25EF";
                        trackerRow.Pitch.color = pitchColor;
                    }


                    // Roll
                    var roll = (int)eulerAngles.z;
                    if (roll >= 180)
                    {
                        roll -= 360;
                    }

                    var rollColor = AngleDeltaColor(roll);
                    if (roll < 0)
                    {
                        trackerRow.Roll.text = (-roll).ToString();
                        trackerRow.Roll.color = rollColor;
                        trackerRow.RollLeft.color = rollColor;
                    }
                    else if (roll > 0)
                    {
                        trackerRow.Roll.text = roll.ToString();
                        trackerRow.Roll.color = rollColor;
                        trackerRow.RollRight.color = rollColor;
                    }
                    else
                    {
                        trackerRow.Roll.text = "\u25EF";
                        trackerRow.Roll.color = rollColor;
                    }
                }
            }
        }
    }

    private TrackerRow CreateRow(string key, string name, string yaw, string pitch, string roll, int row, bool showArrows)
    {
        var y = -row * SpacingY;

        var yawX = SpacingX * 1;
        var pitchX = SpacingX * 2;
        var rollX = SpacingX * 3;

        return new()
        {
            Name = CreateText($"{key}_NAME", name, 0.0f, y),
            // Yaw
            Yaw = CreateText($"{key}_YAW", yaw, yawX, y),
            YawLeft = showArrows ? CreateText($"{key}_YAW_LEFT", "\u2190", yawX - ArrowOffsetX * 0.5f, y) : null,
            YawRight = showArrows ? CreateText($"{key}_YAW_RIGHT", "\u2192", yawX + ArrowOffsetX * 0.5f, y) : null,
            // Pitch
            Pitch = CreateText($"{key}_PITCH", pitch, pitchX, y),
            PitchBack = showArrows ? CreateText($"{key}_PITCH_BACK", "\u2196", pitchX - ArrowOffsetX * 0.5f, y) : null,
            PitchForward = showArrows ? CreateText($"{key}_PITCH_FORWARD", "\u2197", pitchX + ArrowOffsetX * 0.5f, y) : null,
            // Roll
            Roll = CreateText($"{key}_ROLL", roll, 3 * SpacingX, y),
            RollLeft = showArrows ? CreateText($"{key}_ROLL_LEFT", "\u2196", rollX - ArrowOffsetX * 0.5f, y) : null,
            RollRight = showArrows ? CreateText($"{key}_ROLL_RIGHT", "\u2197", rollX + ArrowOffsetX * 0.5f, y) : null,
        };
    }

    private TextMeshPro CreateText(string key, string text, float x, float y)
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
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0.0f);

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
