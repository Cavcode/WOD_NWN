using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.Item.Property;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.PerkService;
using static WOD.Game.Server.Core.NWScript.NWScript;
using Random = WOD.Game.Server.Service.Random;

namespace WOD.Game.Server.Feature.AbilityDefinition.FirstAid
{
    public class StasisFieldAbilityDefinition: FirstAidBaseAbilityDefinition
    {
        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            StasisField1();
            StasisField2();
            StasisField3();

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

        private void Impact(uint activator, uint target, int baseAmount)
        {
            var willpowerMod = GetAbilityModifier(AbilityType.Willpower, activator);
            const float BaseLength = 900f;
            var length = BaseLength + willpowerMod * 30f;

            ApplyEffectToObject(DurationType.Temporary, EffectACIncrease(baseAmount, ArmorClassModiferType.Natural), target, length);
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Ac_Bonus), target);

            TakeStimPack(activator);
        }

        private void StasisField1()
        {
            Builder.Create(FeatType.StasisField1, PerkType.StasisField)
                .Name("Stasis Field I")
                .HasRecastDelay(RecastGroup.StasisField, 30f)
                .HasActivationDelay(2f)
                .RequirementStamina(5)
                .UsesAnimation(Animation.LoopingGetMid)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, target, _, _) =>
                {
                    Impact(activator, target, 2);
                });
        }

        private void StasisField2()
        {
            Builder.Create(FeatType.StasisField2, PerkType.StasisField)
                .Name("Stasis Field II")
                .HasRecastDelay(RecastGroup.StasisField, 30f)
                .HasActivationDelay(2f)
                .RequirementStamina(6)
                .UsesAnimation(Animation.LoopingGetMid)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, target, _, _) =>
                {
                    Impact(activator, target, 4);
                });
        }

        private void StasisField3()
        {
            Builder.Create(FeatType.StasisField3, PerkType.StasisField)
                .Name("Stasis Field III")
                .HasRecastDelay(RecastGroup.StasisField, 30f)
                .HasActivationDelay(2f)
                .RequirementStamina(7)
                .UsesAnimation(Animation.LoopingGetMid)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation(Validation)
                .HasImpactAction((activator, target, _, _) =>
                {
                    Impact(activator, target, 6);
                });
        }
    }
}
