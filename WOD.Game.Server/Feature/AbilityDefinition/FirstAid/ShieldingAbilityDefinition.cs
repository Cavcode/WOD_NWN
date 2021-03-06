using System.Collections.Generic;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.StatusEffectService;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature.AbilityDefinition.FirstAid
{
    public class ShieldingAbilityDefinition: FirstAidBaseAbilityDefinition
    {
        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Shielding1();
            Shielding2();
            Shielding3();
            Shielding4();

            return Builder.Build();
        }

        private string Validation(uint activator, uint target, int level, Location location)
        {
            if (!IsWithinRange(activator, target))
            {
                return "Your target is too far away.";
            }

            if (!HasMedicalSupplies(activator))
            {
                return "You have no stim packs.";
            }

            return string.Empty;
        }

        private void Impact(uint activator, uint target, StatusEffectType statusEffectType)
        {
            var willpowerMod = GetAbilityModifier(AbilityType.Willpower, activator);
            const float BaseLength = 900f;
            var length = BaseLength + willpowerMod * 30f;
            
            StatusEffect.Apply(activator, target, statusEffectType, length);
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Spell_Mantle_Use), target);

            TakeStimPack(activator);
        }

        private void Shielding1()
        {
            Builder.Create(FeatType.Shielding1, PerkType.Shielding)
                .Name("Shielding I")
                .HasRecastDelay(RecastGroup.Shielding, 30f)
                .HasActivationDelay(2f)
                .RequirementStamina(3)
                .UsesAnimation(Animation.LoopingGetMid)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, target, _, _) =>
                {
                    Impact(activator, target, StatusEffectType.Shielding1);
                });
        }

        private void Shielding2()
        {
            Builder.Create(FeatType.Shielding2, PerkType.Shielding)
                .Name("Shielding II")
                .HasRecastDelay(RecastGroup.Shielding, 30f)
                .HasActivationDelay(2f)
                .RequirementStamina(4)
                .UsesAnimation(Animation.LoopingGetMid)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, target, _, _) =>
                {
                    Impact(activator, target, StatusEffectType.Shielding2);
                });
        }

        private void Shielding3()
        {
            Builder.Create(FeatType.Shielding3, PerkType.Shielding)
                .Name("Shielding III")
                .HasRecastDelay(RecastGroup.Shielding, 30f)
                .HasActivationDelay(2f)
                .RequirementStamina(5)
                .UsesAnimation(Animation.LoopingGetMid)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, target, _, _) =>
                {
                    Impact(activator, target, StatusEffectType.Shielding3);
                });
        }

        private void Shielding4()
        {
            Builder.Create(FeatType.Shielding4, PerkType.Shielding)
                .Name("Shielding IV")
                .HasRecastDelay(RecastGroup.Shielding, 30f)
                .HasActivationDelay(2f)
                .RequirementStamina(6)
                .UsesAnimation(Animation.LoopingGetMid)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, target, _, _) =>
                {
                    Impact(activator, target, StatusEffectType.Shielding4);
                });
        }
    }
}
