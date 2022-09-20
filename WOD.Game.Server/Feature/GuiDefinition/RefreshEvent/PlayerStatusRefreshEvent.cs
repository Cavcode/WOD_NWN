using WOD.Game.Server.Service.GuiService;

namespace WOD.Game.Server.Feature.GuiDefinition.RefreshEvent
{
    internal class PlayerStatusRefreshEvent: IGuiRefreshEvent
    {
        internal enum StatType
        {
            HP = 1,
            Resource = 2,
        }

        public StatType Type { get; set; }

        public PlayerStatusRefreshEvent(StatType type)
        {
            Type = type;
        }
    }
}
