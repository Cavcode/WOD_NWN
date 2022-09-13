﻿using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.CombatService;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.SkillService;
using WOD.Game.Server.Service.StatusEffectService;

namespace WOD.Game.Server.Feature.AbilityDefinition.NPC
{
    public class SpikesAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Spikes();

            return _builder.Build();
        }

        private void Spikes()
        {
            _builder.Create(FeatType.Spikes, PerkType.Invalid)
                .Name("Spikes")
                .HasActivationDelay(3.5f)
                .HasRecastDelay(RecastGroup.Spikes, 20f)
                .IsCastedAbility()
                .RequirementStamina(8)
                .HasImpactAction((activator, target, level, location) =>
                {
                    const int DMG = 3;
                    var attackerStat = GetAbilityScore(activator, AbilityType.Might);
                    var attack = Stat.GetAttack(activator, AbilityType.Might, SkillType.Invalid);
                    var defense = Stat.GetDefense(target, CombatDamageType.Physical, AbilityType.Vitality);
                    var defenderStat = GetAbilityScore(target, AbilityType.Vitality);
                    var damage = Combat.CalculateDamage(
                        attack,
                        DMG, 
                        attackerStat, 
                        defense, 
                        defenderStat, 
                        0);

                    ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Piercing), target);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Wallspike), target);
                    StatusEffect.Apply(activator, target, StatusEffectType.Bleed, 45f);
                });
        }

    }
}
