// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace solarxr_protocol.rpc
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct HeartbeatResponse : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_22_10_26(); }
  public static HeartbeatResponse GetRootAsHeartbeatResponse(ByteBuffer _bb) { return GetRootAsHeartbeatResponse(_bb, new HeartbeatResponse()); }
  public static HeartbeatResponse GetRootAsHeartbeatResponse(ByteBuffer _bb, HeartbeatResponse obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public HeartbeatResponse __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartHeartbeatResponse(FlatBufferBuilder builder) { builder.StartTable(0); }
  public static Offset<solarxr_protocol.rpc.HeartbeatResponse> EndHeartbeatResponse(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<solarxr_protocol.rpc.HeartbeatResponse>(o);
  }
}


}
