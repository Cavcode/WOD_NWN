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
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature.AbilityDefinition.TwoHanded
{
    public class DoubleThrustAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            DoubleThrust1(builder);
            DoubleThrust2(builder);
            DoubleThrust3(builder);

            return builder.Build();
        }

        private static string Validation(uint activator, uint target, int level, Location targetLocation)
        {
            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);

            if (!Item.PolearmBaseItemTypes.Contains(GetBaseItemType(weapon)))
            {
                return "This is a polearm ability.";
            }
            else
                return string.Empty;
        }

        private static void ImpactAction(uint activator, uint target, int level, Location targetLocation)
        {
            var dmg = 0.0f;

            // If activator is in stealth mode, force them out of stealth mode.
            if (GetActionMode(activator, ActionMode.Stealth) == true)
                SetActionMode(activator, ActionMode.Stealth, false);

            switch (level)
            {
                case 1:
                    dmg = 1.5f;
                    break;
                case 2:
                    dmg = 4.0f;
                    break;
                case 3:
                    dmg = 6.0f;
                    break;
                default:
                    break;
            }

            var might = GetAbilityModifier(AbilityType.Might, activator);
            var defense = Stat.GetDefense(target, CombatDamageType.Physical);
            var vitality = GetAbilityModifier(AbilityType.Vitality, target);
            var damage = Combat.CalculateDamage(dmg, might, defense, vitality, false);
            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Piercing), target);

            Enmity.ModifyEnmityOnAll(activator, 1);
            CombatPoint.AddCombatPointToAllTagged(activator, SkillType.TwoHanded, 3);
        }

        private static void DoubleThrust1(AbilityBuilder builder)
        {
            builder.Create(FeatType.DoubleThrust1, PerkType.DoubleThrust)
                .Name("Double Thrust I")
                .HasRecastDelay(RecastGroup.DoubleThrust, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(3)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, target, level, targetLocation) =>
                {
                    ImpactAction(activator, target, level, targetLocation);
                    ImpactAction(activator, target, level, targetLocation);
                });
        }
        private static void DoubleThrust2(AbilityBuilder builder)
        {
            builder.Create(FeatType.DoubleThrust2, PerkType.DoubleThrust)
                .Name("Double Thrust II")
                .HasRecastDelay(RecastGroup.DoubleThrust, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(5)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, target, level, targetLocation) =>
                {
                    ImpactAction(activator, target, level, targetLocation);
                    ImpactAction(activator, target, level, targetLocation);
                });
        }
        private static void DoubleThrust3(AbilityBuilder builder)
        {
            builder.Create(FeatType.DoubleThrust3, PerkType.DoubleThrust)
                .Name("Double Thrust III")
                .HasRecastDelay(RecastGroup.DoubleThrust, 60f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(8)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, target, level, targetLocation) =>
                {
                    ImpactAction(activator, target, level, targetLocation);
                    ImpactAction(activator, target, level, targetLocation);
                });
        }
    }
}