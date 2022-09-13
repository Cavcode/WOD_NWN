using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Service.StatusEffectService;

namespace WOD.Game.Server.Service.AbilityService
{
    public class ActiveConcentrationAbility
    {
        public ActiveConcentrationAbility(uint target, FeatType feat, StatusEffectType statusEffectType)
        {
            Target = target;
            Feat = feat;
            StatusEffectType = statusEffectType;
        }

        public uint Target { get; set; }
        public FeatType Feat { get; set; }
        public StatusEffectType StatusEffectType { get; set; }
    }
}
