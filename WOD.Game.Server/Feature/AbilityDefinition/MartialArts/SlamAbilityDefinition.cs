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

namespace WOD.Game.Server.Feature.AbilityDefinition.MartialArts
{
    public class SlamAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            Slam1(builder);
            Slam2(builder);
            Slam3(builder);

            return builder.Build();
        }

        private static string Validation(uint activator, uint target, int level, Location targetLocation)
        {
            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);

            if (!Item.StaffBaseItemTypes.Contains(GetBaseItemType(weapon)))
            {
                return "This is a staff ability.";
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
                    dmg = 2.0f;
                    inflict = true;
                    duration = 30f;
                    break;
                case 2:
                    dmg = 4.5f;
                    inflict = true;
                    duration = 60f;
                    break;
                case 3:
                    dmg = 7.0f;
                    duration = 60f;
                    break;
                default:
                    break;
            }


            var might = GetAbilityModifier(AbilityType.Might, activator);
            var defense = Stat.GetDefense(target, CombatDamageType.Physical);
            var vitality = GetAbilityModifier(AbilityType.Vitality, target);
            var damage = Combat.CalculateDamage(dmg, might, defense, vitality, false);
            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Bludgeoning), target);
            if (inflict) ApplyEffectToObject(DurationType.Temporary, EffectBlindness(), target, duration);

            Enmity.ModifyEnmityOnAll(activator, 1);
            CombatPoint.AddCombatPoint(activator, target, SkillType.MartialArts, 3);
        }

        private static void Slam1(AbilityBuilder builder)
        {
            builder.Create(FeatType.Slam1, PerkType.Slam)
                .Name("Slam I")
                .HasRecastDelay(RecastGroup.Slam, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(3)
                .IsWeaponAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void Slam2(AbilityBuilder builder)
        {
            builder.Create(FeatType.Slam2, PerkType.Slam)
                .Name("Slam II")
                .HasRecastDelay(RecastGroup.Slam, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(4)
                .IsWeaponAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
        private static void Slam3(AbilityBuilder builder)
        {
            builder.Create(FeatType.Slam3, PerkType.Slam)
                .Name("Slam III")
                .HasRecastDelay(RecastGroup.Slam, 30f)
                .HasActivationDelay(2.0f)
                .RequirementStamina(5)
                .IsWeaponAbility()
                .HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }
    }
}