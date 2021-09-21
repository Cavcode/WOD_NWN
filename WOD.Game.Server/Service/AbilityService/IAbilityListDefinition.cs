using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;

namespace WOD.Game.Server.Service.AbilityService
{
    public interface IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities();
    }
}
