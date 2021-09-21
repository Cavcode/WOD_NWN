using System.Collections.Generic;
using WOD.Game.Server.Enumeration;

namespace WOD.Game.Server.Service.PerkService
{
    public interface IPerkListDefinition
    {
        public Dictionary<PerkType, PerkDetail> BuildPerks();
    }
}
