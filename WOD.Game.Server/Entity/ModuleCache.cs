﻿using System.Collections.Generic;
using System.Numerics;

namespace WOD.Game.Server.Entity
{
    public class ModuleCache: EntityBase
    {
        public ModuleCache()
        {
            Id = "WOD_CACHE";
            WalkmeshesByArea = new Dictionary<string, List<Vector3>>();
        }

        public int LastModuleMTime { get; set; }
        public Dictionary<string, List<Vector3>> WalkmeshesByArea { get; set; }
        public Dictionary<string, string> ItemNamesByResref { get; set; }
    }
}