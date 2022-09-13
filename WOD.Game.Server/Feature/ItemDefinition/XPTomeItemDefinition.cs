using System.Collections.Generic;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Feature.DialogDefinition;
using WOD.Game.Server.Feature.GuiDefinition.RefreshEvent;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.ItemService;

namespace WOD.Game.Server.Feature.ItemDefinition
{
    public class XPTomeItemDefinition: IItemListDefinition
    {
        public Dictionary<string, ItemDetail> BuildItems()
        {
            var builder = new ItemBuilder();
            XPTomes(builder);
            PerkRefundTome(builder);

            return builder.Build();
        }

        private static void XPTomes(ItemBuilder builder)
        {
            builder.Create("xp_tome_1", "xp_tome_2", "xp_tome_3", "xp_tome_4")
                .ApplyAction((user, item, target, location) =>
                {
                    SetLocalObject(user, "XP_TOME_OBJECT", item);
                    AssignCommand(user, () => ClearAllActions());

                    Dialog.StartConversation(user, user, nameof(XPTomeDialog));
                });
        }

        private static void PerkRefundTome(ItemBuilder builder)
        {
            builder.Create("refund_tome")
                .ValidationAction((user, item, target, location) =>
                {
                    if (!GetIsPC(user) || GetIsDM(user))
                    {
                        return "Only players may use this item.";
                    }

                    var playerId = GetObjectUUID(user);
                    var dbPlayer = DB.Get<Player>(playerId);

                    if (dbPlayer.NumberPerkResetsAvailable >= 99)
                    {
                        return "You cannot add any more perk resets to your collection.";
                    }

                    return string.Empty;
                })
                .ApplyAction((user, item, target, location) =>
                {
                    var playerId = GetObjectUUID(user);
                    var dbPlayer = DB.Get<Player>(playerId);

                    dbPlayer.NumberPerkResetsAvailable++;

                    DB.Set(dbPlayer);

                    SendMessageToPC(user, $"You gain a reset token. (Total: {dbPlayer.NumberPerkResetsAvailable})");
                    DestroyObject(item);
                    Gui.PublishRefreshEvent(user, new PerkResetAcquiredRefreshEvent());
                });
        }
        
    }
}
