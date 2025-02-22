// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace solarxr_protocol.rpc
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SerialDevice : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_22_10_26(); }
  public static SerialDevice GetRootAsSerialDevice(ByteBuffer _bb) { return GetRootAsSerialDevice(_bb, new SerialDevice()); }
  public static SerialDevice GetRootAsSerialDevice(ByteBuffer _bb, SerialDevice obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SerialDevice __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string Port { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetPortBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetPortBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetPortArray() { return __p.__vector_as_array<byte>(4); }
  public string Name { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNameBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNameArray() { return __p.__vector_as_array<byte>(6); }

  public static Offset<solarxr_protocol.rpc.SerialDevice> CreateSerialDevice(FlatBufferBuilder builder,
      StringOffset portOffset = default(StringOffset),
      StringOffset nameOffset = default(StringOffset)) {
    builder.StartTable(2);
    SerialDevice.AddName(builder, nameOffset);
    SerialDevice.AddPort(builder, portOffset);
    return SerialDevice.EndSerialDevice(builder);
  }

  public static void StartSerialDevice(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddPort(FlatBufferBuilder builder, StringOffset portOffset) { builder.AddOffset(0, portOffset.Value, 0); }
  public static void AddName(FlatBufferBuilder builder, StringOffset nameOffset) { builder.AddOffset(1, nameOffset.Value, 0); }
  public static Offset<solarxr_protocol.rpc.SerialDevice> EndSerialDevice(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<solarxr_protocol.rpc.SerialDevice>(o);
  }
}


}
