﻿using WOD.Game.Server.Core;

namespace WOD.Game.Server.Service.SpaceService
{
    public class ShipDockPoint
    {
        public string Name { get; set; }
        public string PropertyId { get; set; }
        public Location Location { get; set; }
        public bool IsNPC { get; set; }
    }
}
