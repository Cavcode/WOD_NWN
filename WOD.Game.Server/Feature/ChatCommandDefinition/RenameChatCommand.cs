using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Feature.GuiDefinition.Payload;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.ChatCommandService;
using WOD.Game.Server.Service.GuiService;

namespace WOD.Game.Server.Feature.ChatCommandDefinition
{
    public class RenameChatCommand : IChatCommandListDefinition
    {
        public Dictionary<string, ChatCommandDetail> BuildChatCommands()
        {
            var builder = new ChatCommandBuilder();

            builder.Create("rename")
                .Description("Renames the target.")
                .Permissions(AuthorizationLevel.All)
                .RequiresTarget()
                .Action((user, target, location, args) =>
                {
                    var isDM = GetIsDM(user) || GetIsDMPossessed(user);

                    if (!isDM)
                    {
                        if (GetObjectType(target) != ObjectType.Item)
                        {
                            SendMessageToPC(user, "You can only rename items with this command.");
                            return;
                        }
                    }

                    if (GetIsDM(target))
                    {
                        SendMessageToPC(user, "DMs cannot be renamed.");
                        return;
                    }

                    var payload = new RenameItemPayload(target);
                    Gui.TogglePlayerWindow(user, GuiWindowType.RenameItem, payload);
                });

            return builder.Build();
        }
    }
}
