using System.Collections.Generic;

namespace WOD.Game.Server.Service.SpaceService
{
    public interface IShipListDefinition
    {
        public Dictionary<string, ShipDetail> BuildShips();
    }
}
