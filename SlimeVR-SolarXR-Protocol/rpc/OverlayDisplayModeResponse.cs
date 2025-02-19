// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace solarxr_protocol.rpc
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

/// The current state of the overlay's display mode.
public struct OverlayDisplayModeResponse : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_22_10_26(); }
  public static OverlayDisplayModeResponse GetRootAsOverlayDisplayModeResponse(ByteBuffer _bb) { return GetRootAsOverlayDisplayModeResponse(_bb, new OverlayDisplayModeResponse()); }
  public static OverlayDisplayModeResponse GetRootAsOverlayDisplayModeResponse(ByteBuffer _bb, OverlayDisplayModeResponse obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public OverlayDisplayModeResponse __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public bool IsVisible { get { int o = __p.__offset(4); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public bool IsMirrored { get { int o = __p.__offset(6); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }

  public static Offset<solarxr_protocol.rpc.OverlayDisplayModeResponse> CreateOverlayDisplayModeResponse(FlatBufferBuilder builder,
      bool is_visible = false,
      bool is_mirrored = false) {
    builder.StartTable(2);
    OverlayDisplayModeResponse.AddIsMirrored(builder, is_mirrored);
    OverlayDisplayModeResponse.AddIsVisible(builder, is_visible);
    return OverlayDisplayModeResponse.EndOverlayDisplayModeResponse(builder);
  }

  public static void StartOverlayDisplayModeResponse(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddIsVisible(FlatBufferBuilder builder, bool isVisible) { builder.AddBool(0, isVisible, false); }
  public static void AddIsMirrored(FlatBufferBuilder builder, bool isMirrored) { builder.AddBool(1, isMirrored, false); }
  public static Offset<solarxr_protocol.rpc.OverlayDisplayModeResponse> EndOverlayDisplayModeResponse(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<solarxr_protocol.rpc.OverlayDisplayModeResponse>(o);
  }
}


}
