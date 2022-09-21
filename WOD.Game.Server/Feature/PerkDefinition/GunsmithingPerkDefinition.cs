using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.SkillService;

namespace WOD.Game.Server.Feature.PerkDefinition
{
    public class GunsmithingPerkDefinition : IPerkListDefinition
    {
        private readonly PerkBuilder _builder = new();

        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            Synthesis();
            Touch();
            Abilities();
            StarshipBlueprints();
            GunsmithingEquipment();
            EnhancementBlueprints();
            DroidEquipmentBlueprints();
            
            return _builder.Build();
        }

        private void Synthesis()
        {
            _builder.Create(PerkCategoryType.Gunsmithing, PerkType.RapidSynthesisGunsmithing)
                .Name("Rapid Synthesis (Gunsmithing)")

                .AddPerkLevel()
                .Description("Increases progress by 30. (75% success rate)")
                .Price(1)
                .RequirementSkill(SkillType.Gunsmithing, 10);


            _builder.Create(PerkCategoryType.Gunsmithing, PerkType.CarefulSynthesisGunsmithing)
                .Name("Careful Synthesis (Gunsmithing)")

                .AddPerkLevel()
                .Description("Increases progress by 80. (50% success rate)")
                .Price(1)
                .RequirementSkill(SkillType.Gunsmithing, 30);
        }

        private void Touch()
        {
            _builder.Create(PerkCategoryType.Gunsmithing, PerkType.BasicTouchGunsmithing)
                .Name("Basic Touch (Gunsmithing)")

                .AddPerkLevel()
                .Description("Increases quality by 10. (90% success rate)")
                .Price(1)
                .RequirementSkill(SkillType.Gunsmithing, 5);

            _builder.Create(PerkCategoryType.Gunsmithing, PerkType.StandardTouchGunsmithing)
                .Name("Standard Touch (Gunsmithing)")

                .AddPerkLevel()
                .Description("Increases quality by 30. (75% success rate)")
                .Price(1)
                .RequirementSkill(SkillType.Gunsmithing, 15);

            _builder.Create(PerkCategoryType.Gunsmithing, PerkType.PreciseTouchGunsmithing)
                .Name("Precise Touch (Gunsmithing)")

                .AddPerkLevel()
                .Description("Increases quality by 80. (50% success rate)")
                .Price(1)
                .RequirementSkill(SkillType.Gunsmithing, 35);
        }

        private void Abilities()
        {
            _builder.Create(PerkCategoryType.Gunsmithing, PerkType.MastersMendGunsmithing)
                .Name("Master's Mend (Gunsmithing)")

                .AddPerkLevel()
                .Description("Restores item durability by 30.")
                .Price(1)
                .RequirementSkill(SkillType.Gunsmithing, 10);

            _builder.Create(PerkCategoryType.Gunsmithing, PerkType.SteadyHandGunsmithing)
                .Name("Steady Hand (Gunsmithing)")

                .AddPerkLevel()
                .Description("Increases success rate of next synthesis ability to 100%.")
                .Price(1)
                .RequirementSkill(SkillType.Gunsmithing, 20);

            _builder.Create(PerkCategoryType.Gunsmithing, PerkType.MuscleMemoryGunsmithing)
                .Name("Muscle Memory (Gunsmithing)")

                .AddPerkLevel()
                .Description("Increases success rate of next touch ability to 100%.")
                .Price(1)
                .RequirementSkill(SkillType.Gunsmithing, 40);

            _builder.Create(PerkCategoryType.Gunsmithing, PerkType.VenerationGunsmithing)
                .Name("Veneration (Gunsmithing)")

                .AddPerkLevel()
                .Description("Reduces CP cost of Synthesis abilitites by 50% for the next four actions.")
                .Price(1)
                .RequirementSkill(SkillType.Gunsmithing, 25);

            _builder.Create(PerkCategoryType.Gunsmithing, PerkType.WasteNotGunsmithing)
                .Name("Waste Not (Gunsmithing)")

                .AddPerkLevel()
                .Description("Reduces loss of durability by 50% for the next four actions.")
                .Price(1)
                .RequirementSkill(SkillType.Gunsmithing, 8);
        }

        private void StarshipBlueprints()
        {
            _builder.Create(PerkCategoryType.Gunsmithing, PerkType.StarshipBlueprints)
                .Name("Starship Blueprints")

                .AddPerkLevel()
                .Description("Grants access to tier 1 Ship and Module blueprints.")
                .Price(2)
                .GrantsFeat(FeatType.StarshipBlueprints1)

                .AddPerkLevel()
                .Description("Grants access to tier 2 Ship and Module blueprints.")
                .Price(2)
                .RequirementSkill(SkillType.Gunsmithing, 10)
                .GrantsFeat(FeatType.StarshipBlueprints2)

                .AddPerkLevel()
                .Description("Grants access to tier 3 Ship and Module blueprints.")
                .Price(2)
                .RequirementSkill(SkillType.Gunsmithing, 20)
                .GrantsFeat(FeatType.StarshipBlueprints3)

                .AddPerkLevel()
                .Description("Grants access to tier 4 Ship and Module blueprints.")
                .Price(3)
                .RequirementSkill(SkillType.Gunsmithing, 30)
                .GrantsFeat(FeatType.StarshipBlueprints4)

                .AddPerkLevel()
                .Description("Grants access to tier 5 Ship and Module blueprints.")
                .Price(3)
                .RequirementSkill(SkillType.Gunsmithing, 40)
                .GrantsFeat(FeatType.StarshipBlueprints5);
        }


        private void GunsmithingEquipment()
        {
            _builder.Create(PerkCategoryType.Gunsmithing, PerkType.GunsmithingEquipment)
                .Name("Gunsmithing Equipment")

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 1 Gunsmithing equipment.")
                .Price(2)
                .RequirementSkill(SkillType.Gunsmithing, 5)
                .GrantsFeat(FeatType.GunsmithingEquipment1)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 2 Gunsmithing equipment.")
                .Price(3)
                .RequirementSkill(SkillType.Gunsmithing, 15)
                .GrantsFeat(FeatType.GunsmithingEquipment2)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 3 Gunsmithing equipment.")
                .Price(4)
                .RequirementSkill(SkillType.Gunsmithing, 25)
                .GrantsFeat(FeatType.GunsmithingEquipment3)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 4 Gunsmithing equipment.")
                .Price(4)
                .RequirementSkill(SkillType.Gunsmithing, 35)
                .GrantsFeat(FeatType.GunsmithingEquipment4)

                .AddPerkLevel()
                .Description("Grants the ability to equip tier 5 Gunsmithing equipment.")
                .Price(5)
                .RequirementSkill(SkillType.Gunsmithing, 45)
                .GrantsFeat(FeatType.GunsmithingEquipment5);
        }


        private void EnhancementBlueprints()
        {
            _builder.Create(PerkCategoryType.Gunsmithing, PerkType.EnhancementBlueprints)
                .Name("Enhancement Blueprints")

                .AddPerkLevel()
                .Description("Grants access to tier 1 Enhancement blueprints.")
                .Price(1)
                .GrantsFeat(FeatType.EnhancementBlueprints1)

                .AddPerkLevel()
                .Description("Grants access to tier 2 Enhancement blueprints.")
                .Price(1)
                .RequirementSkill(SkillType.Gunsmithing, 10)
                .GrantsFeat(FeatType.EnhancementBlueprints2)

                .AddPerkLevel()
                .Description("Grants access to tier 3 Enhancement blueprints.")
                .Price(2)
                .RequirementSkill(SkillType.Gunsmithing, 20)
                .GrantsFeat(FeatType.EnhancementBlueprints3)

                .AddPerkLevel()
                .Description("Grants access to tier 4 Enhancement blueprints.")
                .Price(3)
                .RequirementSkill(SkillType.Gunsmithing, 30)
                .GrantsFeat(FeatType.EnhancementBlueprints4)

                .AddPerkLevel()
                .Description("Grants access to tier 5 Enhancement blueprints.")
                .Price(3)
                .RequirementSkill(SkillType.Gunsmithing, 40)
                .GrantsFeat(FeatType.EnhancementBlueprints5);
        }

        private void DroidEquipmentBlueprints()
        {
            _builder.Create(PerkCategoryType.Gunsmithing, PerkType.DroidEquipmentBlueprints)
                .Name("Droid Equipment Blueprints")

                .AddPerkLevel()
                .Description("Grants access to tier 1 Droid equipment blueprints.")
                .Price(1)
                .GrantsFeat(FeatType.DroidEquipmentBlueprints1)

                .AddPerkLevel()
                .Description("Grants access to tier 2 Droid equipment blueprints.")
                .Price(1)
                .RequirementSkill(SkillType.Gunsmithing, 10)
                .GrantsFeat(FeatType.DroidEquipmentBlueprints2)

                .AddPerkLevel()
                .Description("Grants access to tier 3 Droid equipment blueprints.")
                .Price(2)
                .RequirementSkill(SkillType.Gunsmithing, 20)
                .GrantsFeat(FeatType.DroidEquipmentBlueprints3)

                .AddPerkLevel()
                .Description("Grants access to tier 4 Droid equipment blueprints.")
                .Price(3)
                .RequirementSkill(SkillType.Gunsmithing, 30)
                .GrantsFeat(FeatType.DroidEquipmentBlueprints4)

                .AddPerkLevel()
                .Description("Grants access to tier 5 Droid equipment blueprints.")
                .Price(3)
                .RequirementSkill(SkillType.Gunsmithing, 40)
                .GrantsFeat(FeatType.DroidEquipmentBlueprints5);
        }
    }
}
