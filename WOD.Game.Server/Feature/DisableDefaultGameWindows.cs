using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature
{
    public static class DisableDefaultGameWindows
    {
        /// <summary>
        /// When the player enters the server, disable default game windows.
        /// In most cases, these windows are replaced with custom versions.
        /// </summary>
        [NWNEventHandler("mod_enter")]
        public static void DisableWindows()
        {
            var player = GetEnteringObject();

            // Spell Book - Completely unused
            SetGuiPanelDisabled(player, GuiPanel.SpellBook, true);

            // Character Sheet - A NUI replacement is used
            SetGuiPanelDisabled(player, GuiPanel.CharacterSheet, true);
        }
    }
}
