using WOD.Game.Server.Core;
using WOD.Game.Server.Core.Beamdog;

namespace WOD.Game.Server.Service.GuiService.Component
{
    public class GuiGroup<T> : GuiWidget<T, GuiGroup<T>>
        where T: IGuiViewModel
    {
        public IGuiWidget Child { get; set; }
        public bool ShowBorder { get; set; }
        public NuiScrollbars Scrollbars { get; set; }

        public GuiGroup()
        {
            ShowBorder = true;
            Scrollbars = NuiScrollbars.Auto;
        }

        public override Json BuildElement()
        {
            var child = Child.ToJson();

            return Nui.Group(child, ShowBorder, Scrollbars);
        }
    }
}
