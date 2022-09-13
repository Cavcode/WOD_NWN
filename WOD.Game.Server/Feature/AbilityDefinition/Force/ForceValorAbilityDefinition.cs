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
    public class ForceValorAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ForceValor1();
            ForceValor2();

            return _builder.Build();
        }

        private void ForceValor1()
        {
            _builder.Create(FeatType.ForceValor1, PerkType.ForceValor)
                .Name("Force Valor I")
                .Level(1)
                .HasRecastDelay(RecastGroup.ForceValor, 30f)
                .RequirementFP(4)
                .IsCastedAbility()
                .HasMaxRange(10f)
                .UsesAnimation(Animation.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .HasImpactAction((activator, target, level, location) =>
                {
                    var willpowerBonus = GetAbilityModifier(AbilityType.Willpower, activator) * 30f;

                    StatusEffect.Apply(activator, target, StatusEffectType.ForceValor1, 60f * 15f + willpowerBonus);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Ac_Bonus), target);

                    CombatPoint.AddCombatPointToAllTagged(activator, SkillType.Force, 3);
                    Enmity.ModifyEnmityOnAll(activator, 250 * level);
                });
        }

        private void ForceValor2()
        {
            _builder.Create(FeatType.ForceValor2, PerkType.ForceValor)
                .Name("Force Valor II")
                .Level(2)
                .HasRecastDelay(RecastGroup.ForceValor, 30f)
                .RequirementFP(6)
                .IsCastedAbility()
                .HasMaxRange(10f)
                .UsesAnimation(Animation.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .HasImpactAction((activator, target, level, location) =>
                {
                    var willpowerBonus = GetAbilityModifier(AbilityType.Willpower, activator) * 30f;

                    StatusEffect.Apply(activator, target, StatusEffectType.ForceValor2, 60f * 15f + willpowerBonus);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Ac_Bonus), target);

                    CombatPoint.AddCombatPointToAllTagged(activator, SkillType.Force, 3);
                    Enmity.ModifyEnmityOnAll(activator, 250 * level);
                });
        }
    }
}
