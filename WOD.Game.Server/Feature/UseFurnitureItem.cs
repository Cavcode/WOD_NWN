using System;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWNX;
using WOD.Game.Server.Feature.DialogDefinition;
using WOD.Game.Server.Service;
using Dialog = WOD.Game.Server.Service.Dialog;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature
{
    public class UseFurnitureItem
    {
        /// <summary>
        /// Before an item is used, if it is a furniture item, cancel the event and start the furniture placement conversation
        /// </summary>
        [NWNEventHandler("item_use_bef")]
        public static void StartFurnitureConversation()
        {
            var player = OBJECT_SELF;
            var area = GetArea(player);
            var item = StringToObject(EventsPlugin.GetEventData("ITEM_OBJECT_ID"));

            if (!Housing.CanPlaceFurniture(player, item))
            {
                return;
            }

            EventsPlugin.SkipEvent();

            var furnitureTypeId = (int)Housing.GetFurnitureTypeFromItem(item);
            var targetLocation = Location(area,
                Vector3(
                    (float)Convert.ToDouble(EventsPlugin.GetEventData("TARGET_POSITION_X")),
                    (float)Convert.ToDouble(EventsPlugin.GetEventData("TARGET_POSITION_Y")),
                    (float)Convert.ToDouble(EventsPlugin.GetEventData("TARGET_POSITION_Z"))),
                0.0f);

            SetLocalInt(player, "TEMP_FURNITURE_TYPE_ID", furnitureTypeId);
            SetLocalObject(player, "TEMP_FURNITURE_OBJECT", item);
            SetLocalLocation(player, "TEMP_FURNITURE_LOCATION", targetLocation);
            Dialog.StartConversation(player, player, nameof(PlayerHouseFurnitureDialog));
        }
    }
}
