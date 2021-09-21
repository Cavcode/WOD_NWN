using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service.StatusEffectService;

namespace WOD.Game.Server.Service.AbilityService
{
    public class ActiveConcentrationAbility
    {
        public ActiveConcentrationAbility(FeatType feat, StatusEffectType statusEffectType)
        {
            Feat = feat;
            StatusEffectType = statusEffectType;
        }

        public FeatType Feat { get; set; }
        public StatusEffectType StatusEffectType { get; set; }
    }
}
