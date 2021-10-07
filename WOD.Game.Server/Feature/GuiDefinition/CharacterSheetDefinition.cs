﻿using WOD.Game.Server.Core.Beamdog;
using WOD.Game.Server.Feature.GuiDefinition.ViewModel;
using WOD.Game.Server.Service.GuiService;

namespace WOD.Game.Server.Feature.GuiDefinition
{
    public class CharacterSheetDefinition : IGuiWindowDefinition
    {
        private readonly GuiWindowBuilder<CharacterSheetViewModel> _builder = new();

        public GuiConstructedWindow BuildWindow()
        {
            _builder.CreateWindow(GuiWindowType.CharacterSheet)
                .BindOnOpened(model => model.OnLoadWindow())
                .SetInitialGeometry(0, 0, 545f, 320f)
                .SetTitle("Character Sheet")
                .AddColumn(col =>
                {
                    col.AddRow(row =>
                    {
                        row.AddLabel()
                            .BindText(model => model.Name)
                            .SetHeight(20f);
                    });

                    col.AddRow(row =>
                    {
                        row.AddSpacer();
                        row.AddImage()
                            .BindResref(model => model.PortraitResref)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Center)
                            .SetAspect(NuiAspect.ExactScaled)
                            .SetWidth(128f)
                            .SetHeight(200f);
                        row.AddSpacer();
                    });

                    col.AddRow(row =>
                    {
                        row.AddSpacer();
                        row.AddButton()
                            .SetText("Change Portrait")
                            .SetHeight(32f)
                            .BindOnClicked(model => model.OnClickChangePortrait());
                        row.AddSpacer();
                    });
                })

                .AddColumn(col =>
                {

                    col.AddRow(row =>
                    {
                        row.AddLabel()
                            .BindText(model => model.CharacterType)
                            .SetHeight(20f);

              
                    });

                    col.AddRow(row =>
                    {
                        row.AddImage()
                            .SetResref("BrujahS")
                            //.SetVerticalAlign(NuiVerticalAlign.Top)
                            //.SetHorizontalAlign(NuiHorizontalAlign.Center)
                            //.SetAspect(NuiAspect.Exact)
                            .SetWidth(200f);
                    });


                    col.AddRow(row =>
                    {
                        row.AddLabel()
                            .SetText("HP")
                            .SetColor(0, 128, 128)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);

                        row.AddLabel()
                            .BindText(model => model.HP)
                            .SetColor(0, 128, 128)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);
                    });

                    col.AddRow(row =>
                    {
                        row.AddLabel()
                            .SetText("VT")
                            .SetColor(255, 0, 0)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);

                        row.AddLabel()
                            .BindText(model => model.FP)
                            .SetColor(255, 0, 0)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);
                    });


                    col.AddRow(row =>
                    {
                        row.AddLabel()
                            .SetText("Might")
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);

                        row.AddLabel()
                            .BindText(model => model.Might)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);
                    });

                    col.AddRow(row =>
                    {
                        row.AddLabel()
                            .SetText("Perception")
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);

                        row.AddLabel()
                            .BindText(model => model.Perception)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);
                    });

                    col.AddRow(row =>
                    {
                        row.AddLabel()
                            .SetText("Vitality")
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);

                        row.AddLabel()
                            .BindText(model => model.Vitality)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);
                    });

                    col.AddRow(row =>
                    {
                        row.AddLabel()
                            .SetText("Willpower")
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);

                        row.AddLabel()
                            .BindText(model => model.Willpower)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);
                    });

                    col.AddRow(row =>
                    {
                        row.AddLabel()
                            .SetText("Social")
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);

                        row.AddLabel()
                            .BindText(model => model.Social)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);
                    });
                })

                .AddColumn(col =>
                {

                    col.AddRow(row =>
                    {
                        row.AddLabel()
                            .BindText(model => model.Race)
                            .SetHeight(20f);
                    });


                    col.AddRow(row =>
                    {
                        row.AddLabel()
                            .SetText("Defense")
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);

                        row.AddLabel()
                            .BindText(model => model.Defense)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);
                    });


                    col.AddRow(row =>
                    {
                        row.AddLabel()
                            .SetText("Evasion")
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);

                        row.AddLabel()
                            .BindText(model => model.Evasion)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);
                    });

                    col.AddRow(row =>
                    {
                        row.AddLabel()
                            .SetText("SP")
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);

                        row.AddLabel()
                            .BindText(model => model.SP)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);
                    });

                    col.AddRow(row =>
                    {
                        row.AddLabel()
                            .SetText("AP")
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);

                        row.AddLabel()
                            .BindText(model => model.AP)
                            .SetVerticalAlign(NuiVerticalAlign.Top)
                            .SetHorizontalAlign(NuiHorizontalAlign.Left);
                    });

                    col.AddRow(row =>
                    {
                        row.AddButton()
                            .SetText("Skills")
                            .SetHeight(32f)
                            .SetWidth(100f)
                            .BindOnClicked(model => model.OnClickSkills());
                        row.AddButton()
                            .SetText("Perks")
                            .SetHeight(32f)
                            .SetWidth(100f)
                            .BindOnClicked(model => model.OnClickPerks());
                    });

                    col.AddRow(row =>
                    {
                        row.AddButton()
                            .SetText("Quests")
                            .SetHeight(32f)
                            .SetWidth(100f)
                            .BindOnClicked(model => model.OnClickQuests());
                        row.AddButton()
                            .SetText("Appearance")
                            .SetHeight(32f)
                            .SetWidth(100f)
                            .BindOnClicked(model => model.OnClickAppearance());
                    });

                    col.AddRow(row =>
                    {
                        row.AddButton()
                            .SetText("Recipes")
                            .SetHeight(32f)
                            .SetWidth(100f)
                            .BindOnClicked(model => model.OnClickRecipes());
                        row.AddButton()
                            .SetText("Key Items")
                            .SetHeight(32f)
                            .SetWidth(100f)
                            .BindOnClicked(model => model.OnClickKeyItems());
                    });

                    col.AddRow(row =>
                    {
                        row.AddButton()
                            .SetText("Achievements")
                            .SetHeight(32f)
                            .SetWidth(100f)
                            .BindOnClicked(model => model.OnClickAchievements());
                        row.AddButton()
                            .SetText("Settings")
                            .SetHeight(32f)
                            .SetWidth(100f)
                            .BindOnClicked(model => model.OnClickSettings());
                    });

                });

            return _builder.Build();
        }
    }
}
