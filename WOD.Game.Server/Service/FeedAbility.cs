using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWNX;
using WOD.Game.Server.Core.NWNX.Enum;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service.ChatCommandService;
using WOD.Game.Server.Service.GuiService;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service
{
    public static class FeedAbility
    {
       
        /// <summary>
        /// When a player uses the "Feed" feat start the feed process.
        /// </summary>
        [NWNEventHandler("feat_use_bef")]
        public static void UseFeedAbility()
        {
            var player = OBJECT_SELF;
            var target = StringToObject(EventsPlugin.GetEventData("TARGET_OBJECT_ID"));
            var feat = (FeatType)Convert.ToInt32(EventsPlugin.GetEventData("FEAT_ID"));
            if (feat != FeatType.Feed) return;
            if (GetObjectType(target) != ObjectType.Creature)
            {
                SendMessageToPC(player, "You can only feed on kine or animals.");
                return;
            }
            if (GetObjectType(target) != ObjectType.Player)
            {
                SendMessageToPC(player, "You cannot feed on other players, diablerist.");
                return;
            }


            // Check to see if the player succeeds in a grapple.


            Gui.TogglePlayerWindow(player, GuiWindowType.FeedBar);
          
        }
    }
}
