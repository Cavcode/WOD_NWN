﻿using System.Runtime.Serialization;
using WOD.Game.Server.Core.Beamdog;
using WOD.Game.Server.Feature.GuiDefinition.ViewModel;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.GuiService;
using WOD.Game.Server.Service.SkillService;

namespace WOD.Game.Server.Feature.GuiDefinition
{
    public class RecipesDefinition : IGuiWindowDefinition
    {
        private readonly GuiWindowBuilder<RecipesViewModel> _builder = new();

        public GuiConstructedWindow BuildWindow()
        {
            _builder.CreateWindow(GuiWindowType.Recipes)
                .BindOnOpened(model => model.OnLoadWindow())
                .SetIsResizable(true)
                .SetInitialGeometry(0, 0, 545f, 295.5f)
                .SetTitle("Recipes")

                .AddColumn(col =>
                {
                    col.AddRow(row =>
                    {
                        var skillsCombo = row.AddComboBox()
                            .BindSelectedIndex(model => model.SelectedSkillId)
                            .SetWidth(200f);

                        skillsCombo.AddOption("Select Skill...", 0);
                        foreach (var (type, detail) in Skill.GetAllSkillsByCategory(SkillCategoryType.Crafting))
                        {
                            skillsCombo.AddOption(detail.Name, (int)type);
                        }
                    });

                    col.AddRow(row =>
                    {
                        row.AddComboBox()
                            .BindSelectedIndex(model => model.SelectedCategoryId)
                            .BindOptions(model => model.Categories)
                            .BindIsEnabled(model => model.IsSkillSelected)
                            .SetWidth(200f);
                    });

                    col.AddRow(row =>
                    {
                        row.AddSpacer();
                        row.AddCheckBox()
                            .BindIsChecked(model => model.ShowAll)
                            .SetText(" Show All");
                    });

                    col.AddRow(row =>
                    {
                        row.AddList(template =>
                        {
                            template.AddToggleButton()
                                .BindIsToggled(model => model.Selections)
                                .BindText(model => model.Recipes)
                                .BindOnClicked(model => model.OnSelectRecipe())
                                .BindColor(model => model.Colors);
                        })
                            .BindRowCount(model => model.Recipes);

                    });
                })
                
                .AddColumn(col =>
                {
                    col.AddRow(row =>
                    {
                        row.AddSpacer();
                        row.AddLabel()
                            .BindText(model => model.RecipeName)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHeight(26f);
                        row.AddSpacer();
                    });

                    col.AddRow(row =>
                    {
                        row.AddSpacer();
                        row.AddLabel()
                            .BindText(model => model.RecipeLevel)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHeight(26f);
                        row.AddSpacer();
                    });

                    col.AddRow(row =>
                    {
                        row.AddSpacer();
                        row.AddLabel()
                            .BindText(model => model.RecipeModSlots)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHeight(26f);
                        row.AddSpacer();
                    });

                    col.AddRow(row =>
                    {
                        row.AddList(template =>
                            {
                                template.AddLabel()
                                    .BindText(model => model.RecipeComponents)
                                    .BindColor(model => model.RecipeComponentColors);
                            })
                            .BindRowCount(model => model.RecipeComponents);

                        row.AddList(template =>
                        {
                            template.AddLabel()
                                .BindText(model => model.RecipeRequirements)
                                .BindColor(model => model.RecipeRequirementColors);
                        })
                            .BindRowCount(model => model.RecipeRequirements);
                    });

                    col.BindIsVisible(model => model.IsRecipeSelected);
                });

            return _builder.Build();
        }
    }
}
