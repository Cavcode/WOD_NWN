//using Random = WOD.Game.Server.Service.Random;

using System.Collections.Generic;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.CombatService;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.SkillService;
using WOD.Game.Server.Service.StatusEffectService;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature.AbilityDefinition.MartialArts
{
    public class ElectricFistAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            ElectricFist1(builder);
            ElectricFist2(builder);
            ElectricFist3(builder);

            return builder.Build();
        }

        private static string Validation(uint activator, uint target, int level, Location targetLocation)
        {
            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);

            if (!Item.KatarBaseItemTypes.Contains(GetBaseItemType(weapon)))
            {
                return "This is a katar ability.";
            }
            else
                return string.Empty;
        }

        private static void ImpactAction(uint activator, uint target, int level, Location targetLocation)
        {
            var dmg = 0.0f;
            var duration = 0f;
            var inflict = false;
            // If activator is in stealth mode, force them out of stealth mode.
            if (GetActionMode(activator, ActionMode.Stealth) == true)
                SetActionMode(activator, ActionMode.Stealth, false);

            switch (level)
            {
                case 1:
                    dmg = 4.0f;
                    inflict = true;
                    duration = 30f;
                    break;
                case 2:
                    dmg = 6.0f;
                    inflict = true;
                    duration = 60f;
                    break;
                case 3:
                    dmg = 9.5f;
                    duration = 60f;
                    break;
                default:
                    break;
            }

            var perception = GetAbilityModifier(AbilityType.Perception, activator);
            var defense = Stat.GetDefense(target, CombatDamageType.Physical) +
                          Stat.GetDefense(target, CombatDamageType.Electrical);
            var vitality = GetAbilityModifier(AbilityType.Vitality, target);
            var damage = Combat.CalculateDamage(dmg, perception, defense, vitality, false);
            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Electrical), target);
            if (inflict) StatusEffect.Apply(activator, target, StatusEffectType.Shock, duration);

            CombatPoint.AddCombatPoint(activator, target, SkillType.MartialArts, 3);
        }

        private static void ElectricFist1(AbilityBuilder builder)
        {
            builder.Create(FeatType.ElectricFist1, PerkType.ElectricFist)
                .Name("Electric Fist I")
                .HasRecastDelay(RecastGroup.ElectricFist, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(3)
                .IsWeaponAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void ElectricFist2(AbilityBuilder builder)
        {
            builder.Create(FeatType.ElectricFist2, PerkType.ElectricFist)
                .Name("Electric Fist II")
                .HasRecastDelay(RecastGroup.ElectricFist, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(4)
                .IsWeaponAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void ElectricFist3(AbilityBuilder builder)
        {
            builder.Create(FeatType.ElectricFist3, PerkType.ElectricFist)
                .Name("Electric Fist III")
                .HasRecastDelay(RecastGroup.ElectricFist, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(5)
                .IsWeaponAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
    }
}