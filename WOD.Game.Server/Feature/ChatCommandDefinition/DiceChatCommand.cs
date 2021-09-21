using System.Collections.Generic;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Feature.DialogDefinition;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.ChatCommandService;

namespace WOD.Game.Server.Feature.ChatCommandDefinition
{
    public class DiceChatCommand: IChatCommandListDefinition
    {
        public Dictionary<string, ChatCommandDetail> BuildChatCommands()
        {
            var builder = new ChatCommandBuilder();

            builder.Create("dice")
                .Description("Opens the dice bag menu.")
                .Permissions(AuthorizationLevel.All)
                .Action((user, target, location, args) =>
                {
                    Dialog.StartConversation(user, user, nameof(DiceDialog));
                });

            return builder.Build();
        }
    }
}
