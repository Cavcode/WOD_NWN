﻿using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.SkillService;

namespace WOD.Game.Server.Feature.PerkDefinition
{
    public class FabricationPerkDefinition : IPerkListDefinition
    {
        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            var builder = new PerkBuilder();
            FurnitureBlueprints(builder);
            StructureBlueprints(builder);
            StarshipBlueprints(builder);
            ModuleBlueprints(builder);

            return builder.Build();
        }

        private void FurnitureBlueprints(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.Fabrication, PerkType.FurnitureBlueprints)
                .Name("Furniture Blueprints")

                .AddPerkLevel()
                .Description("Grants access to tier 1 Furniture blueprints.")
                .Price(1)
                .GrantsFeat(FeatType.FurnitureBlueprints1)

                .AddPerkLevel()
                .Description("Grants access to tier 2 Furniture blueprints.")
                .Price(1)
                .RequirementSkill(SkillType.Fabrication, 10)
                .GrantsFeat(FeatType.FurnitureBlueprints2)

                .AddPerkLevel()
                .Description("Grants access to tier 3 Furniture blueprints.")
                .Price(2)
                .RequirementSkill(SkillType.Fabrication, 20)
                .GrantsFeat(FeatType.FurnitureBlueprints3)

                .AddPerkLevel()
                .Description("Grants access to tier 4 Furniture blueprints.")
                .Price(3)
                .RequirementSkill(SkillType.Fabrication, 30)
                .GrantsFeat(FeatType.FurnitureBlueprints4)

                .AddPerkLevel()
                .Description("Grants access to tier 5 Furniture blueprints.")
                .Price(3)
                .RequirementSkill(SkillType.Fabrication, 40)
                .GrantsFeat(FeatType.FurnitureBlueprints5);
        }

        private void StructureBlueprints(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.Fabrication, PerkType.StructureBlueprints)
                .Name("Structure Blueprints")

                .AddPerkLevel()
                .Description("Grants access to tier 1 Structure blueprints.")
                .Price(2)
                .GrantsFeat(FeatType.StructureBlueprints1)

                .AddPerkLevel()
                .Description("Grants access to tier 2 Structure blueprints.")
                .Price(2)
                .RequirementSkill(SkillType.Fabrication, 10)
                .GrantsFeat(FeatType.StructureBlueprints2)

                .AddPerkLevel()
                .Description("Grants access to tier 3 Structure blueprints.")
                .Price(3)
                .RequirementSkill(SkillType.Fabrication, 20)
                .GrantsFeat(FeatType.StructureBlueprints3)

                .AddPerkLevel()
                .Description("Grants access to tier 4 Structure blueprints.")
                .Price(4)
                .RequirementSkill(SkillType.Fabrication, 30)
                .GrantsFeat(FeatType.StructureBlueprints4)

                .AddPerkLevel()
                .Description("Grants access to tier 5 Structure blueprints.")
                .Price(4)
                .RequirementSkill(SkillType.Fabrication, 40)
                .GrantsFeat(FeatType.StructureBlueprints5);
        }

        private void StarshipBlueprints(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.Fabrication, PerkType.StarshipBlueprints)
                .Name("Starship Blueprints")

                .AddPerkLevel()
                .Description("Grants access to tier 1 Starship blueprints.")
                .Price(5)
                .RequirementSkill(SkillType.Fabrication, 25)
                .GrantsFeat(FeatType.StarshipBlueprints1)

                .AddPerkLevel()
                .Description("Grants access to tier 2 Starship blueprints.")
                .Price(7)
                .RequirementSkill(SkillType.Fabrication, 35)
                .GrantsFeat(FeatType.StarshipBlueprints2)

                .AddPerkLevel()
                .Description("Grants access to tier 3 Starship blueprints.")
                .Price(8)
                .RequirementSkill(SkillType.Fabrication, 45)
                .GrantsFeat(FeatType.StarshipBlueprints3);
        }

        private void ModuleBlueprints(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.Fabrication, PerkType.ModuleBlueprints)
                .Name("Module Blueprints")

                .AddPerkLevel()
                .Description("Grants access to tier 1 Ship Module blueprints.")
                .Price(2)
                .GrantsFeat(FeatType.ModuleBlueprints1)

                .AddPerkLevel()
                .Description("Grants access to tier 2 Ship Module  blueprints.")
                .Price(2)
                .RequirementSkill(SkillType.Fabrication, 10)
                .GrantsFeat(FeatType.ModuleBlueprints2)

                .AddPerkLevel()
                .Description("Grants access to tier 3 Ship Module  blueprints.")
                .Price(3)
                .RequirementSkill(SkillType.Fabrication, 20)
                .GrantsFeat(FeatType.ModuleBlueprints3)

                .AddPerkLevel()
                .Description("Grants access to tier 4 Ship Module  blueprints.")
                .Price(4)
                .RequirementSkill(SkillType.Fabrication, 30)
                .GrantsFeat(FeatType.ModuleBlueprints4)

                .AddPerkLevel()
                .Description("Grants access to tier 5 Ship Module  blueprints.")
                .Price(4)
                .RequirementSkill(SkillType.Fabrication, 40)
                .GrantsFeat(FeatType.ModuleBlueprints5);
        }
    }
}
