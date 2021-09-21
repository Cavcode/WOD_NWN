﻿using System;
using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.CombatService;
using WOD.Game.Server.Service.PerkService;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature.AbilityDefinition.NPC
{
    public class FireBreathAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            FireBreath();

            return _builder.Build();
        }

        private void FireBreath()
        {
            _builder.Create(FeatType.FireBreath, PerkType.Invalid)
                .Name("Fire Breath")
                .HasActivationDelay(2.0f)
                .HasRecastDelay(RecastGroup.FireBreath, 60f)
                .IsCastedAbility()
                .RequirementStamina(6)
                .HasImpactAction((activator, target, level, location) =>
                {
                    var perception = GetAbilityModifier(AbilityType.Perception, activator);
                    var dmg = 3.0f;
                    
                    var coneTarget = GetFirstObjectInShape(Shape.SpellCone, 14.0f, location);
                    while (GetIsObjectValid(coneTarget))
                    {
                        if (GetIsEnemy(coneTarget, activator))
                        {
                            var defense = Stat.GetDefense(target, CombatDamageType.Physical) +
                                          Stat.GetDefense(target, CombatDamageType.Fire);
                            var vitality = GetAbilityModifier(AbilityType.Vitality, coneTarget);
                            var damage = Combat.CalculateDamage(dmg, perception, defense, vitality, false);

                            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Com_Hit_Fire), coneTarget);
                            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Fire), coneTarget);
                        }

                        coneTarget = GetNextObjectInShape(Shape.SpellCone, 14.0f, location);
                    }

                });
        }
    }
}
