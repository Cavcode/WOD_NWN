using System.Collections.Generic;
using WOD.Game.Server.Feature.DialogDefinition;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.ItemService;
using static WOD.Game.Server.Core.NWScript.NWScript;

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
                .ApplyAction((user, item, target, location) =>
                {
                    SetLocalObject(user, "PERK_REFUND_OBJECT", item);
                    AssignCommand(user, () => ClearAllActions());

                    Dialog.StartConversation(user, user, nameof(PerkRefundDialog));
                });
        }
        
    }
}
