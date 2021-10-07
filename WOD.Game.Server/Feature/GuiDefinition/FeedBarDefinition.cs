using WOD.Game.Server.Core.Beamdog;
using WOD.Game.Server.Feature.GuiDefinition.ViewModel;
using WOD.Game.Server.Service.GuiService;

namespace WOD.Game.Server.Feature.GuiDefinition
{
    public class FeedBarDefinition : IGuiWindowDefinition
    {
        private readonly GuiWindowBuilder<FeedBarViewModel> _builder = new();

        public GuiConstructedWindow BuildWindow()
        {
            _builder.CreateWindow(GuiWindowType.CharacterSheet)
                .BindOnOpened(model => model.OnLoadWindow())
                .SetInitialGeometry(0, 60, 545f, 320f)
                .SetTitle("Character Sheet")
                .AddColumn(col =>
                {
                    col.AddRow(row =>
                    {
                        row.AddProgressBar()
                            .BindValue(model => model.Progresses);
                    });
                });

            return _builder.Build();
        }
    }
}+