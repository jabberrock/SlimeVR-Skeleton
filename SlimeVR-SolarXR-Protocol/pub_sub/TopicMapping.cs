// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace solarxr_protocol.pub_sub
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

/// Response for `TopicHandleRequest` or `SubscriptionRequest`.
public struct TopicMapping : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_22_10_26(); }
  public static TopicMapping GetRootAsTopicMapping(ByteBuffer _bb) { return GetRootAsTopicMapping(_bb, new TopicMapping()); }
  public static TopicMapping GetRootAsTopicMapping(ByteBuffer _bb, TopicMapping obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public TopicMapping __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public solarxr_protocol.pub_sub.TopicId? Id { get { int o = __p.__offset(4); return o != 0 ? (solarxr_protocol.pub_sub.TopicId?)(new solarxr_protocol.pub_sub.TopicId()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public solarxr_protocol.pub_sub.TopicHandle? Handle { get { int o = __p.__offset(6); return o != 0 ? (solarxr_protocol.pub_sub.TopicHandle?)(new solarxr_protocol.pub_sub.TopicHandle()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<solarxr_protocol.pub_sub.TopicMapping> CreateTopicMapping(FlatBufferBuilder builder,
      Offset<solarxr_protocol.pub_sub.TopicId> idOffset = default(Offset<solarxr_protocol.pub_sub.TopicId>),
      Offset<solarxr_protocol.pub_sub.TopicHandle> handleOffset = default(Offset<solarxr_protocol.pub_sub.TopicHandle>)) {
    builder.StartTable(2);
    TopicMapping.AddHandle(builder, handleOffset);
    TopicMapping.AddId(builder, idOffset);
    return TopicMapping.EndTopicMapping(builder);
  }

  public static void StartTopicMapping(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddId(FlatBufferBuilder builder, Offset<solarxr_protocol.pub_sub.TopicId> idOffset) { builder.AddOffset(0, idOffset.Value, 0); }
  public static void AddHandle(FlatBufferBuilder builder, Offset<solarxr_protocol.pub_sub.TopicHandle> handleOffset) { builder.AddOffset(1, handleOffset.Value, 0); }
  public static Offset<solarxr_protocol.pub_sub.TopicMapping> EndTopicMapping(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<solarxr_protocol.pub_sub.TopicMapping>(o);
  }
}


}
