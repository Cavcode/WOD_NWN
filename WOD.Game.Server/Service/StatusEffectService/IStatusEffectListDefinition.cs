using System.Collections.Generic;
using WOD.Game.Server.Enumeration;

namespace WOD.Game.Server.Service.StatusEffectService
{
    public interface IStatusEffectListDefinition
    {
        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects();
    }
}
