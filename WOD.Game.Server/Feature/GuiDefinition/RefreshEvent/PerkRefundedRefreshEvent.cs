using WOD.Game.Server.Service.GuiService;
using WOD.Game.Server.Service.PerkService;

namespace WOD.Game.Server.Feature.GuiDefinition.RefreshEvent
{
    public class PerkRefundedRefreshEvent: IGuiRefreshEvent
    {
        public PerkType Type { get; set; }

        public PerkRefundedRefreshEvent(PerkType type)
        {
            Type = type;
        }
    }
}
