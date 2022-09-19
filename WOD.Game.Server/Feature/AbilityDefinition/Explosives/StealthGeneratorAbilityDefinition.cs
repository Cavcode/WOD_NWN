using System.Collections.Generic;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.SkillService;

namespace WOD.Game.Server.Feature.AbilityDefinition.Devices
{
    public class StealthGeneratorAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            StealthGenerator1();
            StealthGenerator2();
            StealthGenerator3();

            return _builder.Build();
        }

        [NWNEventHandler("harvester_used")]
        [NWNEventHandler("pc_damaged")]
        public static void ClearInvisibility()
        {
            RemoveEffect(OBJECT_SELF, EffectTypeScript.Invisibility, EffectTypeScript.ImprovedInvisibility);
        }

        private void StealthGenerator1()
        {
            _builder.Create(FeatType.StealthGenerator1, PerkType.StealthGenerator)
                .Name("Stealth Generator I")
                .Level(1)
                .HasRecastDelay(RecastGroup.StealthGenerator, 180f)
                .HasActivationDelay(2f)
                .RequirementStamina(4)
                .UsesAnimation(Animation.Crouch)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasImpactAction((activator, _, _, targetLocation) =>
                {
                    ApplyEffectToObject(DurationType.Temporary, EffectInvisibility(InvisibilityType.Normal), activator, 30f);

                    Enmity.ModifyEnmityOnAll(activator, 450);
                    CombatPoint.AddCombatPointToAllTagged(activator, SkillType.Devices, 3);
                });
        }

        private void StealthGenerator2()
        {
            _builder.Create(FeatType.StealthGenerator2, PerkType.StealthGenerator)
                .Name("Stealth Generator II")
                .Level(2)
                .HasRecastDelay(RecastGroup.StealthGenerator, 180f)
                .HasActivationDelay(2f)
                .RequirementStamina(6)
                .UsesAnimation(Animation.Crouch)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasImpactAction((activator, _, _, targetLocation) =>
                {
                    ApplyEffectToObject(DurationType.Temporary, EffectInvisibility(InvisibilityType.Normal), activator, 60f);

                    Enmity.ModifyEnmityOnAll(activator, 750);
                    CombatPoint.AddCombatPointToAllTagged(activator, SkillType.Devices, 3);
                });
        }

        private void StealthGenerator3()
        {
            _builder.Create(FeatType.StealthGenerator3, PerkType.StealthGenerator)
                .Name("Stealth Generator III")
                .Level(3)
                .HasRecastDelay(RecastGroup.StealthGenerator, 180f)
                .HasActivationDelay(2f)
                .RequirementStamina(8)
                .UsesAnimation(Animation.Crouch)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasImpactAction((activator, _, _, targetLocation) =>
                {
                    ApplyEffectToObject(DurationType.Temporary, EffectInvisibility(InvisibilityType.Normal), activator, 120f);

                    Enmity.ModifyEnmityOnAll(activator, 950);
                    CombatPoint.AddCombatPointToAllTagged(activator, SkillType.Devices, 3);
                });
        }


    }
}
