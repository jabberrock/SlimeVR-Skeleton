// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace solarxr_protocol.datatypes
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

/// A unique ID for the device. IDs are not guaranteed to be the same after
/// the connection is terminated.
public struct DeviceId : IFlatbufferObject
{
  private Struct __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public void __init(int _i, ByteBuffer _bb) { __p = new Struct(_i, _bb); }
  public DeviceId __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public byte Id { get { return __p.bb.Get(__p.bb_pos + 0); } }

  public static Offset<solarxr_protocol.datatypes.DeviceId> CreateDeviceId(FlatBufferBuilder builder, byte Id) {
    builder.Prep(1, 1);
    builder.PutByte(Id);
    return new Offset<solarxr_protocol.datatypes.DeviceId>(builder.Offset);
  }
}


}
