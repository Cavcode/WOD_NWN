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

namespace WOD.Game.Server.Feature.AbilityDefinition.Ranged
{
    public class ExplosiveTossAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            ExplosiveToss1(builder);
            ExplosiveToss2(builder);
            ExplosiveToss3(builder);

            return builder.Build();
        }

        private static string Validation(uint activator, uint target, int level, Location targetLocation)
        {
            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);

            if (!Item.ThrowingWeaponBaseItemTypes.Contains(GetBaseItemType(weapon)))
            {
                return "This is a throwing ability.";
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
                    dmg = 5.5f;
                    break;
                case 2:
                    dmg = 7.5f;
                    break;
                case 3:
                    dmg = 11.0f;
                    break;
                default:
                    break;
            }

            var count = 0;
            var creature = GetFirstObjectInShape(Shape.Sphere, RadiusSize.Medium, GetLocation(target), true, ObjectType.Creature);
            while (GetIsObjectValid(creature) && count < 3)
            {
                if (GetDistanceBetween(target, creature) <= 3f)
                {
                    var might = GetAbilityModifier(AbilityType.Might, activator);
                    var defense = Stat.GetDefense(target, CombatDamageType.Physical);
                    var vitality = GetAbilityModifier(AbilityType.Vitality, target);
                    var damage = Combat.CalculateDamage(dmg, might, defense, vitality, false);
                    ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Slashing), creature);

                    CombatPoint.AddCombatPoint(activator, creature, SkillType.Ranged, 3);

                    count++;
                }
                creature = GetNextObjectInShape(Shape.Sphere, RadiusSize.Medium, GetLocation(target), true, ObjectType.Creature);
            }
        }

        private static void ExplosiveToss1(AbilityBuilder builder)
        {
            builder.Create(FeatType.ExplosiveToss1, PerkType.ExplosiveToss)
                .Name("Explosive Toss I")
                .HasRecastDelay(RecastGroup.ExplosiveToss, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(3)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void ExplosiveToss2(AbilityBuilder builder)
        {
            builder.Create(FeatType.ExplosiveToss2, PerkType.ExplosiveToss)
                .Name("Explosive Toss II")
                .HasRecastDelay(RecastGroup.ExplosiveToss, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(4)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void ExplosiveToss3(AbilityBuilder builder)
        {
            builder.Create(FeatType.ExplosiveToss3, PerkType.ExplosiveToss)
                .Name("Explosive Toss III")
                .HasRecastDelay(RecastGroup.ExplosiveToss, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(5)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
    }
}