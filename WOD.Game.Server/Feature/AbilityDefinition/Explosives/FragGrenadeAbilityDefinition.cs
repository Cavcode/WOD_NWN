﻿using System.Collections.Generic;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.CombatService;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.SkillService;
using WOD.Game.Server.Service.StatusEffectService;
using Random = WOD.Game.Server.Service.Random;

namespace WOD.Game.Server.Feature.AbilityDefinition.Devices
{
    public class FragGrenadeAbilityDefinition: ExplosiveBaseAbilityDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            FragGrenade1();
            FragGrenade2();
            FragGrenade3();

            return _builder.Build();
        }
        
        private void Impact(uint activator, uint target, int dmg, int bleedChance, float bleedLength)
        {
            if (GetFactionEqual(activator, target))
                return;

            dmg += Combat.GetAbilityDamageBonus(activator, SkillType.Devices);

            var attackerStat = GetAbilityScore(activator, AbilityType.Dexterity);
            var defenderStat = GetAbilityScore(target, AbilityType.Vitality);
            var attack = Stat.GetAttack(activator, AbilityType.Dexterity, SkillType.Devices);
            var defense = Stat.GetDefense(target, CombatDamageType.Physical, AbilityType.Vitality);
            var damage = Combat.CalculateDamage(
                attack,
                dmg, 
                attackerStat, 
                defense, 
                defenderStat, 
                0);

            if (Random.D100(1) <= bleedChance)
            {
                StatusEffect.Apply(activator, target, StatusEffectType.Bleed, bleedLength);
            }

            AssignCommand(activator, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Fire), target);
            });

            CombatPoint.AddCombatPoint(activator, target, SkillType.Devices, 3);
            Enmity.ModifyEnmity(activator, target, 320);
        }

        private void FragGrenade1()
        {
            _builder.Create(FeatType.FragGrenade1, PerkType.FragGrenade)
                .Name("Frag Grenade I")
                .Level(1)
                .HasRecastDelay(RecastGroup.Grenades, 30f)
                .HasActivationDelay(1f)
                .UsesAnimation(Animation.ThrowGrenade)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasMaxRange(15f)
                .HasCustomValidation(ExplosiveValidation)
                .HasImpactAction((activator, _, _, location) =>
                {
                    ExplosiveImpact(activator, location, EffectVisualEffect(VisualEffect.Fnf_Fireball), "explosion2", RadiusSize.Large, (target) =>
                    {
                        Impact(activator, target, 6, 0, 0f);
                    });
                });
        }

        private void FragGrenade2()
        {
            _builder.Create(FeatType.FragGrenade2, PerkType.FragGrenade)
                .Name("Frag Grenade II")
                .Level(2)
                .HasRecastDelay(RecastGroup.Grenades, 30f)
                .HasActivationDelay(1f)
                .UsesAnimation(Animation.ThrowGrenade)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasMaxRange(15f)
                .HasCustomValidation(ExplosiveValidation)
                .HasImpactAction((activator, _, _, location) =>
                {
                    ExplosiveImpact(activator, location, EffectVisualEffect(VisualEffect.Fnf_Fireball), "explosion2", RadiusSize.Large, (target) =>
                    {
                        Impact(activator, target, 10, 30, 30f);
                    });
                });
        }

        private void FragGrenade3()
        {
            _builder.Create(FeatType.FragGrenade3, PerkType.FragGrenade)
                .Name("Frag Grenade III")
                .Level(3)
                .HasRecastDelay(RecastGroup.Grenades, 30f)
                .HasActivationDelay(1f)
                .UsesAnimation(Animation.ThrowGrenade)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasMaxRange(15f)
                .HasCustomValidation(ExplosiveValidation)
                .HasImpactAction((activator, _, _, location) =>
                {
                    ExplosiveImpact(activator, location, EffectVisualEffect(VisualEffect.Fnf_Fireball), "explosion2", RadiusSize.Large, (target) =>
                    {
                        Impact(activator, target, 16, 50, 60f);
                    });
                });
        }
    }
}
