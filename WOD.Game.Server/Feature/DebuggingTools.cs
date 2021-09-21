using WOD.Game.Server.Core;
using WOD.Game.Server.Service;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature
{
    public static class DebuggingTools
    {
        [NWNEventHandler("test2")]
        public static void KillMe()
        {
            var player = GetLastUsedBy();
            
            Space.ApplyShipDamage(player, player, 999);
        }

    }
}
