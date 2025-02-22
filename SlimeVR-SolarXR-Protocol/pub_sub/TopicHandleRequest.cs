// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace solarxr_protocol.pub_sub
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

/// Request to get the `FeatureHandle` from a `FeatureId`. This is useful for reducing
/// bandwidth, since `FeatureId` can be large.
public struct TopicHandleRequest : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_22_10_26(); }
  public static TopicHandleRequest GetRootAsTopicHandleRequest(ByteBuffer _bb) { return GetRootAsTopicHandleRequest(_bb, new TopicHandleRequest()); }
  public static TopicHandleRequest GetRootAsTopicHandleRequest(ByteBuffer _bb, TopicHandleRequest obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public TopicHandleRequest __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public solarxr_protocol.pub_sub.TopicId? Id { get { int o = __p.__offset(4); return o != 0 ? (solarxr_protocol.pub_sub.TopicId?)(new solarxr_protocol.pub_sub.TopicId()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<solarxr_protocol.pub_sub.TopicHandleRequest> CreateTopicHandleRequest(FlatBufferBuilder builder,
      Offset<solarxr_protocol.pub_sub.TopicId> idOffset = default(Offset<solarxr_protocol.pub_sub.TopicId>)) {
    builder.StartTable(1);
    TopicHandleRequest.AddId(builder, idOffset);
    return TopicHandleRequest.EndTopicHandleRequest(builder);
  }

  public static void StartTopicHandleRequest(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddId(FlatBufferBuilder builder, Offset<solarxr_protocol.pub_sub.TopicId> idOffset) { builder.AddOffset(0, idOffset.Value, 0); }
  public static Offset<solarxr_protocol.pub_sub.TopicHandleRequest> EndTopicHandleRequest(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<solarxr_protocol.pub_sub.TopicHandleRequest>(o);
  }
}


}
