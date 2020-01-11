﻿using Unreal.Core.Attributes;
using Unreal.Core.Contracts;
using Unreal.Core.Models;
using Unreal.Core.Models.Enums;

namespace FortniteReplayReader.Models.NetFieldExports
{
    [NetFieldExportGroup("/Script/FortniteGame.FortVehicleSeatComponent", minimalParseMode: ParseMode.Debug)]
    public class SeatComponent : INetFieldExportGroup
    {
        [NetFieldExport("PlayerSlots", RepLayoutCmdType.Property)]
        public DebuggingObject PlayerSlots { get; set; }

        [NetFieldExport("PlayerEntryTime", RepLayoutCmdType.Property)]
        public DebuggingObject PlayerEntryTime { get; set; }

        [NetFieldExport("WeaponComponent", RepLayoutCmdType.Property)]
        public DebuggingObject WeaponComponent { get; set; }
    }

    [NetFieldExportGroup("/Script/FortniteGame.FortVehicleSeatWeaponComponent", minimalParseMode: ParseMode.Debug)]
    public class WeaponSeatComponent : INetFieldExportGroup
    {
        [NetFieldExport("bWeaponEquipped", RepLayoutCmdType.PropertyBool)]
        public bool bWeaponEquipped { get; set; }

        [NetFieldExport("AmmoInClip", RepLayoutCmdType.Property)]
        public DebuggingObject AmmoInClip { get; set; }

        [NetFieldExport("LastFireTime", RepLayoutCmdType.Property)]
        public DebuggingObject LastFireTime { get; set; }

        [NetFieldExport("bHasPrevious", RepLayoutCmdType.PropertyBool)]
        public bool bHasPrevious { get; set; }
    }
}