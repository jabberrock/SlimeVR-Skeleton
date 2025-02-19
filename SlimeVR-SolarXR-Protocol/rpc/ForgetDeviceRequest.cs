// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace solarxr_protocol.rpc
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct ForgetDeviceRequest : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_22_10_26(); }
  public static ForgetDeviceRequest GetRootAsForgetDeviceRequest(ByteBuffer _bb) { return GetRootAsForgetDeviceRequest(_bb, new ForgetDeviceRequest()); }
  public static ForgetDeviceRequest GetRootAsForgetDeviceRequest(ByteBuffer _bb, ForgetDeviceRequest obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public ForgetDeviceRequest __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string MacAddress { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMacAddressBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetMacAddressBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetMacAddressArray() { return __p.__vector_as_array<byte>(4); }

  public static Offset<solarxr_protocol.rpc.ForgetDeviceRequest> CreateForgetDeviceRequest(FlatBufferBuilder builder,
      StringOffset mac_addressOffset = default(StringOffset)) {
    builder.StartTable(1);
    ForgetDeviceRequest.AddMacAddress(builder, mac_addressOffset);
    return ForgetDeviceRequest.EndForgetDeviceRequest(builder);
  }

  public static void StartForgetDeviceRequest(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddMacAddress(FlatBufferBuilder builder, StringOffset macAddressOffset) { builder.AddOffset(0, macAddressOffset.Value, 0); }
  public static Offset<solarxr_protocol.rpc.ForgetDeviceRequest> EndForgetDeviceRequest(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<solarxr_protocol.rpc.ForgetDeviceRequest>(o);
  }
}


}
