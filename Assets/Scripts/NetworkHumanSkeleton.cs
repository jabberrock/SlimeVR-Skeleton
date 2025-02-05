using Google.FlatBuffers;
using solarxr_protocol;
using solarxr_protocol.data_feed;
using solarxr_protocol.data_feed.device_data;
using solarxr_protocol.data_feed.tracker;
using solarxr_protocol.datatypes;
using solarxr_protocol.datatypes.math;
using solarxr_protocol.pub_sub;
using solarxr_protocol.rpc;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class NetworkHumanSkeleton : MonoBehaviour
{
    public GameObject Camera;

    private readonly Uri serverUri = new("ws://localhost:21110");
    private const int DataFeedUpdateDelayInMs = 20;
    private const float BoneRadius = 0.05f;

    private readonly CancellationTokenSource cancellationTokenSource = new();
    private readonly Dictionary<BodyPart, GameObject> boneObjects = new();

    void Start()
    {
        _ = RunNetworking(cancellationTokenSource.Token);
    }

    private void OnDestroy()
    {
        cancellationTokenSource.Cancel();
    }

    private async Awaitable RunNetworking(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await NetworkLoop(cancellationToken);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }

    private async Awaitable NetworkLoop(CancellationToken cancellationToken)
    {
        using var client = new ClientWebSocket();

        while (true)
        {
            while (true)
            {
                Debug.Log($"Connecting to {serverUri}...");
                await client.ConnectAsync(serverUri, cancellationToken);
                if (client.State == WebSocketState.Open)
                {
                    break;
                }

                Debug.Log($"Failed to connect to {serverUri}");
                await Awaitable.WaitForSecondsAsync(5.0f);

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }

            Debug.Log($"Connected to {serverUri}");

            await SendSubDataFeedMsg(client, cancellationToken);
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            while (client.State == WebSocketState.Open)
            {
                var receiveBuffer = new byte[1024 * 1024]; // 1MB

                var receiveResult = await client.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), cancellationToken);
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                if (!receiveResult.EndOfMessage)
                {
                    Debug.Log($"Received message that does not fit in buffer, ignoring message");
                    while (true)
                    {
                        var nextReceiveResult = await client.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), cancellationToken);
                        if (cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }

                        if (nextReceiveResult.EndOfMessage)
                        {
                            break;
                        }
                    }

                    Debug.Log($"Reached end of message");
                    continue;
                }

                switch (receiveResult.MessageType)
                {
                    case WebSocketMessageType.Binary:
                        var messageBundle = MessageBundle.GetRootAsMessageBundle(new ByteBuffer(receiveBuffer));
                        ProcessMessageBundle(messageBundle);
                        break;

                    case WebSocketMessageType.Text:
                        Debug.Log($"Received text message, ignoring message");
                        Debug.Log(Encoding.UTF8.GetString(new ReadOnlySpan<byte>(receiveBuffer, 0, receiveResult.Count)));
                        break;

                    case WebSocketMessageType.Close:
                        Debug.Log($"Received close message");
                        break;
                }
            }
        }
    }

    private async Awaitable SendSubDataFeedMsg(ClientWebSocket client, CancellationToken cancellationToken)
    {
        var buffer = new byte[1024 * 1024];

        var builder = new FlatBufferBuilder(new ByteBuffer(buffer));

        DeviceDataMask.StartDeviceDataMask(builder);
        var deviceDataMask = DeviceDataMask.EndDeviceDataMask(builder);

        TrackerDataMask.StartTrackerDataMask(builder);
        var syntheticTrackersMask = TrackerDataMask.EndTrackerDataMask(builder);

        DataFeedConfig.StartDataFeedConfig(builder);
        DataFeedConfig.AddMinimumTimeSinceLast(builder, DataFeedUpdateDelayInMs);
        DataFeedConfig.AddDataMask(builder, deviceDataMask);
        DataFeedConfig.AddSyntheticTrackersMask(builder, syntheticTrackersMask);
        DataFeedConfig.AddBoneMask(builder, true);
        var dataFeedConfig = DataFeedConfig.EndDataFeedConfig(builder);

        var dataFeedsVector = StartDataFeed.CreateDataFeedsVector(builder, new[] { dataFeedConfig });

        StartDataFeed.StartStartDataFeed(builder);
        StartDataFeed.AddDataFeeds(builder, dataFeedsVector);
        var startDataFeed = StartDataFeed.EndStartDataFeed(builder);

        DataFeedMessageHeader.StartDataFeedMessageHeader(builder);
        DataFeedMessageHeader.AddMessageType(builder, DataFeedMessage.StartDataFeed);
        DataFeedMessageHeader.AddMessage(builder, startDataFeed.Value);
        var dataFeedMsgHeader = DataFeedMessageHeader.EndDataFeedMessageHeader(builder);

        var dataFeedMsgsVector = MessageBundle.CreateDataFeedMsgsVector(builder, new[] { dataFeedMsgHeader });
        var rpcMsgsVector = MessageBundle.CreateRpcMsgsVector(builder, new Offset<RpcMessageHeader>[0]);
        var pubSubMsgsVector = MessageBundle.CreatePubSubMsgsVector(builder, new Offset<PubSubHeader>[0]);

        MessageBundle.StartMessageBundle(builder);
        MessageBundle.AddDataFeedMsgs(builder, dataFeedMsgsVector);
        MessageBundle.AddRpcMsgs(builder, rpcMsgsVector);
        MessageBundle.AddPubSubMsgs(builder, pubSubMsgsVector);
        var messageBundle = MessageBundle.EndMessageBundle(builder);

        builder.Finish(messageBundle.Value);

        Debug.Log("Sending data feed subscription message...");

        await client.SendAsync(
            new ReadOnlyMemory<byte>(
                buffer,
                builder.DataBuffer.Position,
                builder.DataBuffer.Length - builder.DataBuffer.Position),
            WebSocketMessageType.Binary,
            true,
            cancellationToken);
    }

    private void ProcessMessageBundle(MessageBundle messageBundle)
    {
        for (var i = 0; i < messageBundle.DataFeedMsgsLength; ++i)
        {
            var dataFeedMsg = messageBundle.DataFeedMsgs(i);
            if (dataFeedMsg.HasValue && dataFeedMsg.Value.MessageType == DataFeedMessage.DataFeedUpdate)
            {
                var dataFeedUpdate = dataFeedMsg.Value.MessageAsDataFeedUpdate();
                ProcessDataFeedUpdate(dataFeedUpdate);
            }
        }
    }

    private void ProcessDataFeedUpdate(DataFeedUpdate update)
    {
        if (update.BonesLength == 0)
        {
            return;
        }

        var headPosition = Vector3.zero;
        var headInvRotation = Quaternion.identity;
        for (var i = 0; i < update.BonesLength; ++i)
        {
            var optionalBone = update.Bones(i);
            if (optionalBone.HasValue)
            {
                var bone = optionalBone.Value;
                // Use the neck bone which is rotated in the correct way, unlike the head bone
                if (bone.BodyPart == BodyPart.NECK &&
                    bone.HeadPositionG.HasValue &&
                    bone.RotationG.HasValue)
                {
                    headPosition = RHSToLHSVector3(bone.HeadPositionG.Value);
                    headInvRotation = Quaternion.Inverse(RHSToLHSQuaternion(bone.RotationG.Value));
                }
            }
        }

        for (var i = 0; i < update.BonesLength; ++i)
        {
            var optionalBone = update.Bones(i);
            if (optionalBone.HasValue)
            {
                var bone = optionalBone.Value;

                // The head bone blocks the view
                if (bone.BodyPart == BodyPart.HEAD)
                {
                    continue;
                }

                if (!boneObjects.TryGetValue(bone.BodyPart, out var boneObject))
                {
                    boneObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    boneObject.name = bone.BodyPart.ToString();
                    boneObject.transform.parent = transform;

                    boneObjects.Add(bone.BodyPart, boneObject);
                }

                if (bone.HeadPositionG.HasValue && bone.RotationG.HasValue)
                {
                    boneObject.transform.SetLocalPositionAndRotation(
                        RHSToLHSVector3(bone.HeadPositionG.Value)
                            - (bone.BoneLength * 0.5f) * (RHSToLHSQuaternion(bone.RotationG.Value) * Vector3.up)
                            - headPosition,
                        RHSToLHSQuaternion(bone.RotationG.Value));

                    boneObject.transform.localScale = new Vector3(BoneRadius, bone.BoneLength * 0.5f, BoneRadius);
                }
            }
        }

        if (Camera != null)
        {
            transform.SetPositionAndRotation(
                Camera.transform.position,
                Camera.transform.rotation * headInvRotation);
        }
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
