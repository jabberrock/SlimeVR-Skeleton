// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace solarxr_protocol.data_feed
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

/// Requests for the other party to send `data_feeds`.
/// For example, GUI requests for position data to be sent from server.
///
/// When sending a new `StartFeed`, the old data feeds should stop being sent.
/// We still support multiple data feeds at the same time, because `data_feeds`
/// is a list.
///
/// Multiple data feeds are useful to get data at different frequencies.
public struct StartDataFeed : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_22_10_26(); }
  public static StartDataFeed GetRootAsStartDataFeed(ByteBuffer _bb) { return GetRootAsStartDataFeed(_bb, new StartDataFeed()); }
  public static StartDataFeed GetRootAsStartDataFeed(ByteBuffer _bb, StartDataFeed obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public StartDataFeed __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public solarxr_protocol.data_feed.DataFeedConfig? DataFeeds(int j) { int o = __p.__offset(4); return o != 0 ? (solarxr_protocol.data_feed.DataFeedConfig?)(new solarxr_protocol.data_feed.DataFeedConfig()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DataFeedsLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<solarxr_protocol.data_feed.StartDataFeed> CreateStartDataFeed(FlatBufferBuilder builder,
      VectorOffset data_feedsOffset = default(VectorOffset)) {
    builder.StartTable(1);
    StartDataFeed.AddDataFeeds(builder, data_feedsOffset);
    return StartDataFeed.EndStartDataFeed(builder);
  }

  public static void StartStartDataFeed(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDataFeeds(FlatBufferBuilder builder, VectorOffset dataFeedsOffset) { builder.AddOffset(0, dataFeedsOffset.Value, 0); }
  public static VectorOffset CreateDataFeedsVector(FlatBufferBuilder builder, Offset<solarxr_protocol.data_feed.DataFeedConfig>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDataFeedsVectorBlock(FlatBufferBuilder builder, Offset<solarxr_protocol.data_feed.DataFeedConfig>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateDataFeedsVectorBlock(FlatBufferBuilder builder, ArraySegment<Offset<solarxr_protocol.data_feed.DataFeedConfig>> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateDataFeedsVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<Offset<solarxr_protocol.data_feed.DataFeedConfig>>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartDataFeedsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<solarxr_protocol.data_feed.StartDataFeed> EndStartDataFeed(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<solarxr_protocol.data_feed.StartDataFeed>(o);
  }
}


}
