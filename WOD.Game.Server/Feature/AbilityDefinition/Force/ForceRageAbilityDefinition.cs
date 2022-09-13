using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.SkillService;
using WOD.Game.Server.Service.StatusEffectService;

namespace WOD.Game.Server.Feature.AbilityDefinition.Force
{
    public class ForceRageAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ForceRage1();
            ForceRage2();

            return _builder.Build();
        }

        private void ForceRage1()
        {
            _builder.Create(FeatType.ForceRage1, PerkType.ForceRage)
                .Name("Force Rage I")
                .Level(1)
                .HasRecastDelay(RecastGroup.ForceRage, 30f)
                .RequirementFP(4)
                .IsCastedAbility()
                .HasMaxRange(10f)
                .UsesAnimation(Animation.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .HasImpactAction((activator, target, level, location) =>
                {
                    var willpowerBonus = GetAbilityModifier(AbilityType.Willpower, activator) * 30f;

                    StatusEffect.Apply(activator, target, StatusEffectType.ForceRage1, 60f * 15f + willpowerBonus);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Negative_Energy), target);

                    CombatPoint.AddCombatPointToAllTagged(activator, SkillType.Force, 3);
                    Enmity.ModifyEnmityOnAll(activator, 250 * level);
                });
        }

        private void ForceRage2()
        {
            _builder.Create(FeatType.ForceRage2, PerkType.ForceRage)
                .Name("Force Rage II")
                .Level(2)
                .HasRecastDelay(RecastGroup.ForceRage, 30f)
                .RequirementFP(6)
                .IsCastedAbility()
                .HasMaxRange(10f)
                .UsesAnimation(Animation.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .HasImpactAction((activator, target, level, location) =>
                {
                    var willpowerBonus = GetAbilityModifier(AbilityType.Willpower, activator) * 30f;

                    StatusEffect.Apply(activator, target, StatusEffectType.ForceRage2, 60f * 15f + willpowerBonus);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Negative_Energy), target);

                    CombatPoint.AddCombatPointToAllTagged(activator, SkillType.Force, 3);
                    Enmity.ModifyEnmityOnAll(activator, 250 * level);
                });
        }
    }
}
