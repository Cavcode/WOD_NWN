using WOD.Game.Server.Service.GuiService;

namespace WOD.Game.Server.Feature.GuiDefinition.RefreshEvent
{
    public class QuestCompletedRefreshEvent: IGuiRefreshEvent
    {
        public string QuestId { get; set; }

        public QuestCompletedRefreshEvent(string questId)
        {
            QuestId = questId;
        }
    }
}
