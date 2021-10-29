﻿using System.Collections.Generic;
using WOD.Game.Server.Service.HousingService;

namespace WOD.Game.Server.Entity
{
    public class PlayerHouse: EntityBase
    {
        public PlayerHouse()
        {
            Furnitures = new Dictionary<string, PlayerHouseFurniture>();
        }


        [Indexed]
        public string CustomName { get; set; }
        [Indexed]
        public PlayerHouseType HouseType { get; set; }
        public Dictionary<string, PlayerHouseFurniture> Furnitures { get; set; }
        public Dictionary<string, PlayerHousePermission> PlayerPermissions { get; set; }
    }

    public class PlayerHouseFurniture
    {
        public FurnitureType FurnitureType { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Orientation { get; set; }
        public string CustomName { get; set; }
    }

    public class PlayerHousePermission
    {
        public bool CanEnter { get; set; }
        public bool CanPlaceFurniture { get; set; }
        public bool CanAdjustPermissions { get; set; }
    }
}
