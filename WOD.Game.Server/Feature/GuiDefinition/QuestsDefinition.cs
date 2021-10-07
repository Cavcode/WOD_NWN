using WOD.Game.Server.Feature.GuiDefinition.ViewModel;
using WOD.Game.Server.Service.GuiService;

namespace WOD.Game.Server.Feature.GuiDefinition
{
    public class QuestsDefinition : IGuiWindowDefinition
    {
        private readonly GuiWindowBuilder<QuestsViewModel> _builder = new();

        public GuiConstructedWindow BuildWindow()
        {
            _builder.CreateWindow(GuiWindowType.Quests);

            return _builder.Build();
        }
    }
}
