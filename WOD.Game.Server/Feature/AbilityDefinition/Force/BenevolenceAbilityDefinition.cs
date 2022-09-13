using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.SkillService;
using Random = WOD.Game.Server.Service.Random;

namespace WOD.Game.Server.Feature.AbilityDefinition.Force
{
    public class BenevolenceAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Benevolence1();
            Benevolence2();
            Benevolence3();

            return _builder.Build();
        }

        private void Impact(uint activator, uint target, int baseAmount)
        {
            var willBonus = GetAbilityModifier(AbilityType.Willpower, activator);
            var amount = baseAmount + willBonus * 8 + Random.D10(1);

            ApplyEffectToObject(DurationType.Instant, EffectHeal(amount), target);
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Healing_M), target);

            Enmity.ModifyEnmityOnAll(activator, 300 + amount);
            CombatPoint.AddCombatPointToAllTagged(activator, SkillType.Force, 3);
        }

        private void Benevolence1()
        {
            _builder.Create(FeatType.Benevolence1, PerkType.Benevolence)
                .Name("Benevolence I")
                .Level(1)
                .HasRecastDelay(RecastGroup.Benevolence, 6f)
                .RequirementFP(4)
                .IsCastedAbility()
                .HasMaxRange(10f)
                .UsesAnimation(Animation.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .HasImpactAction((activator, target, level, location) =>
                {
                    Impact(activator, target, 40);
                });
        }

        private void Benevolence2()
        {
            _builder.Create(FeatType.Benevolence2, PerkType.Benevolence)
                .Name("Benevolence II")
                .Level(2)
                .HasRecastDelay(RecastGroup.Benevolence, 6f)
                .RequirementFP(6)
                .IsCastedAbility()
                .HasMaxRange(10f)
                .UsesAnimation(Animation.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .HasImpactAction((activator, target, level, location) =>
                {
                    Impact(activator, target, 80);
                });
        }

        private void Benevolence3()
        {
            _builder.Create(FeatType.Benevolence3, PerkType.Benevolence)
                .Name("Benevolence III")
                .Level(3)
                .HasRecastDelay(RecastGroup.Benevolence, 6f)
                .RequirementFP(8)
                .IsCastedAbility()
                .HasMaxRange(10f)
                .UsesAnimation(Animation.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .HasImpactAction((activator, target, level, location) =>
                {
                    Impact(activator, target, 120);
                });
        }
    }
}
