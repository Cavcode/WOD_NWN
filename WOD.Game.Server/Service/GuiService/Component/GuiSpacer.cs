using WOD.Game.Server.Core;
using WOD.Game.Server.Core.Beamdog;

namespace WOD.Game.Server.Service.GuiService.Component
{
    public class GuiSpacer<T> : GuiWidget<T, GuiSpacer<T>>
        where T: IGuiViewModel
    {
        public override Json BuildElement()
        {
            return Nui.Spacer();
        }
    }
}
