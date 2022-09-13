using WOD.Game.Server.Service.GuiService;
using WOD.Game.Server.Service.PerkService;

namespace WOD.Game.Server.Feature.GuiDefinition.RefreshEvent
{
    public class PerkAcquiredRefreshEvent: IGuiRefreshEvent
    {
        public PerkType Type { get; set; }

        public PerkAcquiredRefreshEvent(PerkType type)
        {
            Type = type;
        }
    }
}
