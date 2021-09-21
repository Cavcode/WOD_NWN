using System.Collections.Generic;

namespace WOD.Game.Server.Service.SpaceService
{
    public interface IShipModuleListDefinition
    {
        public Dictionary<string, ShipModuleDetail> BuildShipModules();
    }
}
