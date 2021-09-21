using System.Collections.Generic;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.PerkService;
using static WOD.Game.Server.Core.NWScript.NWScript;
using Random = WOD.Game.Server.Service.Random;

namespace WOD.Game.Server.Feature.AbilityDefinition.FirstAid
{
    public class BactaRecoveryAbilityDefinition: FirstAidBaseAbilityDefinition
    {
        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            BactaRecovery1();
            BactaRecovery2();
            BactaRecovery3();

            return Builder.Build();
        }

        private string Validation(uint activator, uint target, int level, Location location)
        {
            if (!HasMedicalSupplies(activator))
            {
                return "You have no medical supplies.";
            }

            return string.Empty;
        }

        private void Impact(uint activator, int baseAmount)
        {
            var willpowerMod = GetAbilityModifier(AbilityType.Willpower, activator);
            var distance = 3f + Perk.GetEffectivePerkLevel(activator, PerkType.RangedHealing);
            var party = Party.GetAllPartyMembersWithinRange(activator, distance);

            foreach (var member in party)
            {
                var amount = baseAmount + willpowerMod * 5 + Random.D10(1);

                ApplyEffectToObject(DurationType.Instant, EffectHeal(amount), member);
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Head_Heal), member);
            }

            TakeMedicalSupplies(activator);
        }

        private void BactaRecovery1()
        {
            Builder.Create(FeatType.BactaRecovery1, PerkType.BactaRecovery)
                .Name("Bacta Recovery I")
                .HasRecastDelay(RecastGroup.BactaRecovery, 30f)
                .HasActivationDelay(2f)
                .RequirementStamina(5)
                .UsesAnimation(Animation.LoopingGetMid)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, _, _, _) =>
                {
                    Impact(activator, 15);
                });
        }
        private void BactaRecovery2()
        {
            Builder.Create(FeatType.BactaRecovery2, PerkType.BactaRecovery)
                .Name("Bacta Recovery II")
                .HasRecastDelay(RecastGroup.BactaRecovery, 30f)
                .HasActivationDelay(2f)
                .RequirementStamina(6)
                .UsesAnimation(Animation.LoopingGetMid)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, _, _, _) =>
                {
                    Impact(activator, 60);
                });
        }
        private void BactaRecovery3()
        {
            Builder.Create(FeatType.BactaRecovery3, PerkType.BactaRecovery)
                .Name("Bacta Recovery III")
                .HasRecastDelay(RecastGroup.BactaRecovery, 30f)
                .HasActivationDelay(2f)
                .RequirementStamina(7)
                .UsesAnimation(Animation.LoopingGetMid)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, _, _, _) =>
                {
                    Impact(activator, 100);
                });
        }
    }
}
