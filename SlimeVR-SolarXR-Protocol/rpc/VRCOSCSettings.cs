// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace solarxr_protocol.rpc
{

using global::System;
using global::System.Collections.Generic;
using global::Google.FlatBuffers;

/// OSC Settings specific to VRChat
public struct VRCOSCSettings : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_22_10_26(); }
  public static VRCOSCSettings GetRootAsVRCOSCSettings(ByteBuffer _bb) { return GetRootAsVRCOSCSettings(_bb, new VRCOSCSettings()); }
  public static VRCOSCSettings GetRootAsVRCOSCSettings(ByteBuffer _bb, VRCOSCSettings obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public VRCOSCSettings __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public solarxr_protocol.rpc.OSCSettings? OscSettings { get { int o = __p.__offset(4); return o != 0 ? (solarxr_protocol.rpc.OSCSettings?)(new solarxr_protocol.rpc.OSCSettings()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public solarxr_protocol.rpc.OSCTrackersSetting? Trackers { get { int o = __p.__offset(6); return o != 0 ? (solarxr_protocol.rpc.OSCTrackersSetting?)(new solarxr_protocol.rpc.OSCTrackersSetting()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<solarxr_protocol.rpc.VRCOSCSettings> CreateVRCOSCSettings(FlatBufferBuilder builder,
      Offset<solarxr_protocol.rpc.OSCSettings> osc_settingsOffset = default(Offset<solarxr_protocol.rpc.OSCSettings>),
      Offset<solarxr_protocol.rpc.OSCTrackersSetting> trackersOffset = default(Offset<solarxr_protocol.rpc.OSCTrackersSetting>)) {
    builder.StartTable(2);
    VRCOSCSettings.AddTrackers(builder, trackersOffset);
    VRCOSCSettings.AddOscSettings(builder, osc_settingsOffset);
    return VRCOSCSettings.EndVRCOSCSettings(builder);
  }

  public static void StartVRCOSCSettings(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddOscSettings(FlatBufferBuilder builder, Offset<solarxr_protocol.rpc.OSCSettings> oscSettingsOffset) { builder.AddOffset(0, oscSettingsOffset.Value, 0); }
  public static void AddTrackers(FlatBufferBuilder builder, Offset<solarxr_protocol.rpc.OSCTrackersSetting> trackersOffset) { builder.AddOffset(1, trackersOffset.Value, 0); }
  public static Offset<solarxr_protocol.rpc.VRCOSCSettings> EndVRCOSCSettings(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<solarxr_protocol.rpc.VRCOSCSettings>(o);
  }
}


}
