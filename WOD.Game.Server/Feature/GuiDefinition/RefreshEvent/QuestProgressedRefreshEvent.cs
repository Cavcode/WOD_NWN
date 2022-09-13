using WOD.Game.Server.Service.GuiService;

namespace WOD.Game.Server.Feature.GuiDefinition.RefreshEvent
{
    public class QuestProgressedRefreshEvent: IGuiRefreshEvent
    {
        public string QuestId { get; set; }

        public QuestProgressedRefreshEvent(string questId)
        {
            QuestId = questId;
        }
    }
}
