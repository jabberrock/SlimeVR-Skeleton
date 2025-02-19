// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace solarxr_protocol.rpc
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

public struct TapDetectionSettings : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_22_10_26(); }
  public static TapDetectionSettings GetRootAsTapDetectionSettings(ByteBuffer _bb) { return GetRootAsTapDetectionSettings(_bb, new TapDetectionSettings()); }
  public static TapDetectionSettings GetRootAsTapDetectionSettings(ByteBuffer _bb, TapDetectionSettings obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public TapDetectionSettings __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public float? FullResetDelay { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float?)null; } }
  public bool? FullResetEnabled { get { int o = __p.__offset(6); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool?)null; } }
  public byte? FullResetTaps { get { int o = __p.__offset(8); return o != 0 ? __p.bb.Get(o + __p.bb_pos) : (byte?)null; } }
  public float? YawResetDelay { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float?)null; } }
  public bool? YawResetEnabled { get { int o = __p.__offset(12); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool?)null; } }
  public byte? YawResetTaps { get { int o = __p.__offset(14); return o != 0 ? __p.bb.Get(o + __p.bb_pos) : (byte?)null; } }
  public float? MountingResetDelay { get { int o = __p.__offset(16); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float?)null; } }
  public bool? MountingResetEnabled { get { int o = __p.__offset(18); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool?)null; } }
  public byte? MountingResetTaps { get { int o = __p.__offset(20); return o != 0 ? __p.bb.Get(o + __p.bb_pos) : (byte?)null; } }
  /// If true, disables reset behavior of tap detection and sends a
  /// TapDetectionSetupNotification, each time 2 taps are detected on any tracker
  public bool? SetupMode { get { int o = __p.__offset(22); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool?)null; } }
  public byte? NumberTrackersOverThreshold { get { int o = __p.__offset(24); return o != 0 ? __p.bb.Get(o + __p.bb_pos) : (byte?)null; } }

  public static Offset<solarxr_protocol.rpc.TapDetectionSettings> CreateTapDetectionSettings(FlatBufferBuilder builder,
      float? full_reset_delay = null,
      bool? full_reset_enabled = null,
      byte? full_reset_taps = null,
      float? yaw_reset_delay = null,
      bool? yaw_reset_enabled = null,
      byte? yaw_reset_taps = null,
      float? mounting_reset_delay = null,
      bool? mounting_reset_enabled = null,
      byte? mounting_reset_taps = null,
      bool? setup_mode = null,
      byte? number_trackers_over_threshold = null) {
    builder.StartTable(11);
    TapDetectionSettings.AddMountingResetDelay(builder, mounting_reset_delay);
    TapDetectionSettings.AddYawResetDelay(builder, yaw_reset_delay);
    TapDetectionSettings.AddFullResetDelay(builder, full_reset_delay);
    TapDetectionSettings.AddNumberTrackersOverThreshold(builder, number_trackers_over_threshold);
    TapDetectionSettings.AddSetupMode(builder, setup_mode);
    TapDetectionSettings.AddMountingResetTaps(builder, mounting_reset_taps);
    TapDetectionSettings.AddMountingResetEnabled(builder, mounting_reset_enabled);
    TapDetectionSettings.AddYawResetTaps(builder, yaw_reset_taps);
    TapDetectionSettings.AddYawResetEnabled(builder, yaw_reset_enabled);
    TapDetectionSettings.AddFullResetTaps(builder, full_reset_taps);
    TapDetectionSettings.AddFullResetEnabled(builder, full_reset_enabled);
    return TapDetectionSettings.EndTapDetectionSettings(builder);
  }

  public static void StartTapDetectionSettings(FlatBufferBuilder builder) { builder.StartTable(11); }
  public static void AddFullResetDelay(FlatBufferBuilder builder, float? fullResetDelay) { builder.AddFloat(0, fullResetDelay); }
  public static void AddFullResetEnabled(FlatBufferBuilder builder, bool? fullResetEnabled) { builder.AddBool(1, fullResetEnabled); }
  public static void AddFullResetTaps(FlatBufferBuilder builder, byte? fullResetTaps) { builder.AddByte(2, fullResetTaps); }
  public static void AddYawResetDelay(FlatBufferBuilder builder, float? yawResetDelay) { builder.AddFloat(3, yawResetDelay); }
  public static void AddYawResetEnabled(FlatBufferBuilder builder, bool? yawResetEnabled) { builder.AddBool(4, yawResetEnabled); }
  public static void AddYawResetTaps(FlatBufferBuilder builder, byte? yawResetTaps) { builder.AddByte(5, yawResetTaps); }
  public static void AddMountingResetDelay(FlatBufferBuilder builder, float? mountingResetDelay) { builder.AddFloat(6, mountingResetDelay); }
  public static void AddMountingResetEnabled(FlatBufferBuilder builder, bool? mountingResetEnabled) { builder.AddBool(7, mountingResetEnabled); }
  public static void AddMountingResetTaps(FlatBufferBuilder builder, byte? mountingResetTaps) { builder.AddByte(8, mountingResetTaps); }
  public static void AddSetupMode(FlatBufferBuilder builder, bool? setupMode) { builder.AddBool(9, setupMode); }
  public static void AddNumberTrackersOverThreshold(FlatBufferBuilder builder, byte? numberTrackersOverThreshold) { builder.AddByte(10, numberTrackersOverThreshold); }
  public static Offset<solarxr_protocol.rpc.TapDetectionSettings> EndTapDetectionSettings(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<solarxr_protocol.rpc.TapDetectionSettings>(o);
  }
}


}
