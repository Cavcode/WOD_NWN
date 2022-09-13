using System.Collections.Generic;

namespace WOD.Game.Server.Service.PerkService
{
    public interface IPerkListDefinition
    {
        public Dictionary<PerkType, PerkDetail> BuildPerks();
    }
}
