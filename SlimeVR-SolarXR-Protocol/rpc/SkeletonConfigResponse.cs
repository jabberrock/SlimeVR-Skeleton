// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace solarxr_protocol.rpc
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct SkeletonConfigResponse : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_22_10_26(); }
  public static SkeletonConfigResponse GetRootAsSkeletonConfigResponse(ByteBuffer _bb) { return GetRootAsSkeletonConfigResponse(_bb, new SkeletonConfigResponse()); }
  public static SkeletonConfigResponse GetRootAsSkeletonConfigResponse(ByteBuffer _bb, SkeletonConfigResponse obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public SkeletonConfigResponse __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public solarxr_protocol.rpc.SkeletonPart? SkeletonParts(int j) { int o = __p.__offset(4); return o != 0 ? (solarxr_protocol.rpc.SkeletonPart?)(new solarxr_protocol.rpc.SkeletonPart()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int SkeletonPartsLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<solarxr_protocol.rpc.SkeletonConfigResponse> CreateSkeletonConfigResponse(FlatBufferBuilder builder,
      VectorOffset skeleton_partsOffset = default(VectorOffset)) {
    builder.StartTable(1);
    SkeletonConfigResponse.AddSkeletonParts(builder, skeleton_partsOffset);
    return SkeletonConfigResponse.EndSkeletonConfigResponse(builder);
  }

  public static void StartSkeletonConfigResponse(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddSkeletonParts(FlatBufferBuilder builder, VectorOffset skeletonPartsOffset) { builder.AddOffset(0, skeletonPartsOffset.Value, 0); }
  public static VectorOffset CreateSkeletonPartsVector(FlatBufferBuilder builder, Offset<solarxr_protocol.rpc.SkeletonPart>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateSkeletonPartsVectorBlock(FlatBufferBuilder builder, Offset<solarxr_protocol.rpc.SkeletonPart>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateSkeletonPartsVectorBlock(FlatBufferBuilder builder, ArraySegment<Offset<solarxr_protocol.rpc.SkeletonPart>> data) { builder.StartVector(4, data.Count, 4); builder.Add(data); return builder.EndVector(); }
  public static VectorOffset CreateSkeletonPartsVectorBlock(FlatBufferBuilder builder, IntPtr dataPtr, int sizeInBytes) { builder.StartVector(1, sizeInBytes, 1); builder.Add<Offset<solarxr_protocol.rpc.SkeletonPart>>(dataPtr, sizeInBytes); return builder.EndVector(); }
  public static void StartSkeletonPartsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<solarxr_protocol.rpc.SkeletonConfigResponse> EndSkeletonConfigResponse(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<solarxr_protocol.rpc.SkeletonConfigResponse>(o);
  }
}


}
