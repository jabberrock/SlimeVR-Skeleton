using Google.FlatBuffers;
using solarxr_protocol;
using solarxr_protocol.data_feed;
using solarxr_protocol.data_feed.device_data;
using solarxr_protocol.data_feed.tracker;
using solarxr_protocol.pub_sub;
using solarxr_protocol.rpc;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class NetworkHumanSkeleton : MonoBehaviour
{
    public GameObject Camera;

    public class DataFeedEntry
    {
        public DataFeedUpdate Update;
        public Vector3 CameraPosition;
        public Quaternion CameraRotation;
    }

    public DataFeedEntry LatestDataFeedUpdate { get; private set; }
    
    private readonly Uri serverUri = new("ws://localhost:21110");
    private const int FPS = 60;
    private const int DataFeedUpdateDelayInMs = 1000 / FPS;

    private readonly CancellationTokenSource cancellationTokenSource = new();

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

        TrackerDataMask.StartTrackerDataMask(builder);
        TrackerDataMask.AddInfo(builder, true);
        TrackerDataMask.AddStatus(builder, true);
        TrackerDataMask.AddPosition(builder, true);
        TrackerDataMask.AddRotation(builder, true);
        TrackerDataMask.AddRotationIdentityAdjusted(builder, true);
        TrackerDataMask.AddRotationReferenceAdjusted(builder, true);
        var physicalTrackersMask = TrackerDataMask.EndTrackerDataMask(builder);

        DeviceDataMask.StartDeviceDataMask(builder);
        DeviceDataMask.AddTrackerData(builder, physicalTrackersMask);
        DeviceDataMask.AddDeviceData(builder, true);
        var deviceDataMask = DeviceDataMask.EndDeviceDataMask(builder);

        TrackerDataMask.StartTrackerDataMask(builder);
        TrackerDataMask.AddInfo(builder, true);
        TrackerDataMask.AddStatus(builder, true);
        TrackerDataMask.AddPosition(builder, true);
        TrackerDataMask.AddRotationIdentityAdjusted(builder, true);
        TrackerDataMask.AddRotationReferenceAdjusted(builder, true);
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
                LatestDataFeedUpdate = new()
                {
                    Update = dataFeedMsg.Value.MessageAsDataFeedUpdate(),
                    CameraPosition = Camera.transform.position,
                    CameraRotation = Camera.transform.rotation
                };
            }
        }
    }
}
