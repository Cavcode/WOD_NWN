using WOD.Game.Server.Service.AchievementService;
using WOD.Game.Server.Service.GuiService;

namespace WOD.Game.Server.Feature.GuiDefinition.RefreshEvent
{
    public class AchievementUnlockedRefreshEvent : IGuiRefreshEvent
    {
        public AchievementType Type { get; set; }
    }
}
