using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.CombatService;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.SkillService;
using WOD.Game.Server.Service.StatusEffectService;

namespace WOD.Game.Server.Feature.AbilityDefinition.Force
{
    public class CreepingTerrorAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            CreepingTerror1();
            CreepingTerror2();
            CreepingTerror3();

            return _builder.Build();
        }

        private void CreepingTerror1()
        {
            _builder.Create(FeatType.CreepingTerror1, PerkType.CreepingTerror)
                .Name("Creeping Terror I")
                .Level(1)
                .HasRecastDelay(RecastGroup.CreepingTerror, 30f)
                .RequirementFP(4)
                .IsCastedAbility()
                .HasMaxRange(10f)
                .UsesAnimation(Animation.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .IsHostileAbility()
                .HasImpactAction((activator, target, level, location) =>
                {
                    StatusEffect.Apply(activator, target, StatusEffectType.CreepingTerror, 24f, 1);
                });
        }

        private void CreepingTerror2()
        {
            _builder.Create(FeatType.CreepingTerror2, PerkType.CreepingTerror)
                .Name("Creeping Terror II")
                .Level(2)
                .HasRecastDelay(RecastGroup.CreepingTerror, 30f)
                .RequirementFP(6)
                .IsCastedAbility()
                .HasMaxRange(10f)
                .UsesAnimation(Animation.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .IsHostileAbility()
                .HasImpactAction((activator, target, level, location) =>
                {
                    StatusEffect.Apply(activator, target, StatusEffectType.CreepingTerror, 24f, 2);
                });
        }

        private void CreepingTerror3()
        {
            _builder.Create(FeatType.CreepingTerror3, PerkType.CreepingTerror)
                .Name("Creeping Terror III")
                .Level(3)
                .HasRecastDelay(RecastGroup.CreepingTerror, 30f)
                .RequirementFP(8)
                .IsCastedAbility()
                .HasMaxRange(10f)
                .UsesAnimation(Animation.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .IsHostileAbility()
                .HasImpactAction((activator, target, level, location) =>
                {
                    StatusEffect.Apply(activator, target, StatusEffectType.CreepingTerror, 24f, 3);
                });
        }
    }
}
