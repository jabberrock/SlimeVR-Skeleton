// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace solarxr_protocol.rpc
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct OpenSerialRequest : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_22_10_26(); }
  public static OpenSerialRequest GetRootAsOpenSerialRequest(ByteBuffer _bb) { return GetRootAsOpenSerialRequest(_bb, new OpenSerialRequest()); }
  public static OpenSerialRequest GetRootAsOpenSerialRequest(ByteBuffer _bb, OpenSerialRequest obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public OpenSerialRequest __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  /// Automatically pick the first serial device available
  public bool Auto { get { int o = __p.__offset(4); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public string Port { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetPortBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetPortBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetPortArray() { return __p.__vector_as_array<byte>(6); }

  public static Offset<solarxr_protocol.rpc.OpenSerialRequest> CreateOpenSerialRequest(FlatBufferBuilder builder,
      bool auto = false,
      StringOffset portOffset = default(StringOffset)) {
    builder.StartTable(2);
    OpenSerialRequest.AddPort(builder, portOffset);
    OpenSerialRequest.AddAuto(builder, auto);
    return OpenSerialRequest.EndOpenSerialRequest(builder);
  }

  public static void StartOpenSerialRequest(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddAuto(FlatBufferBuilder builder, bool auto) { builder.AddBool(0, auto, false); }
  public static void AddPort(FlatBufferBuilder builder, StringOffset portOffset) { builder.AddOffset(1, portOffset.Value, 0); }
  public static Offset<solarxr_protocol.rpc.OpenSerialRequest> EndOpenSerialRequest(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<solarxr_protocol.rpc.OpenSerialRequest>(o);
  }
}


}
