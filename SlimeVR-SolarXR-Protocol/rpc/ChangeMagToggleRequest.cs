// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace solarxr_protocol.rpc
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

/// If no tracker ID is given, it's the setting for every tracker/device
public struct ChangeMagToggleRequest : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_22_10_26(); }
  public static ChangeMagToggleRequest GetRootAsChangeMagToggleRequest(ByteBuffer _bb) { return GetRootAsChangeMagToggleRequest(_bb, new ChangeMagToggleRequest()); }
  public static ChangeMagToggleRequest GetRootAsChangeMagToggleRequest(ByteBuffer _bb, ChangeMagToggleRequest obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public ChangeMagToggleRequest __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public solarxr_protocol.datatypes.TrackerId? TrackerId { get { int o = __p.__offset(4); return o != 0 ? (solarxr_protocol.datatypes.TrackerId?)(new solarxr_protocol.datatypes.TrackerId()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public bool Enable { get { int o = __p.__offset(6); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }

  public static Offset<solarxr_protocol.rpc.ChangeMagToggleRequest> CreateChangeMagToggleRequest(FlatBufferBuilder builder,
      Offset<solarxr_protocol.datatypes.TrackerId> tracker_idOffset = default(Offset<solarxr_protocol.datatypes.TrackerId>),
      bool enable = false) {
    builder.StartTable(2);
    ChangeMagToggleRequest.AddTrackerId(builder, tracker_idOffset);
    ChangeMagToggleRequest.AddEnable(builder, enable);
    return ChangeMagToggleRequest.EndChangeMagToggleRequest(builder);
  }

  public static void StartChangeMagToggleRequest(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddTrackerId(FlatBufferBuilder builder, Offset<solarxr_protocol.datatypes.TrackerId> trackerIdOffset) { builder.AddOffset(0, trackerIdOffset.Value, 0); }
  public static void AddEnable(FlatBufferBuilder builder, bool enable) { builder.AddBool(1, enable, false); }
  public static Offset<solarxr_protocol.rpc.ChangeMagToggleRequest> EndChangeMagToggleRequest(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<solarxr_protocol.rpc.ChangeMagToggleRequest>(o);
  }
}


}
