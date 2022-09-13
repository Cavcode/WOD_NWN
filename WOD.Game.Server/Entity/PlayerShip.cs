using System.Collections.Generic;
using WOD.Game.Server.Service.SpaceService;

namespace WOD.Game.Server.Entity
{
    public class PlayerShip: EntityBase
    {
        public PlayerShip()
        {
            PlayerHotBars = new Dictionary<string, string>();
        }

        [Indexed]
        public string OwnerPlayerId { get; set; }
        [Indexed]
        public string PropertyId { get; set; }
        public string SerializedItem { get; set; }
        public ShipStatus Status { get; set; }
        public Dictionary<string, string> PlayerHotBars { get; set; }
    }
}
