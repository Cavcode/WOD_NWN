using System.Collections.Generic;

namespace WOD.Game.Server.Service.ChatCommandService
{
    public interface IChatCommandListDefinition
    {
        public Dictionary<string, ChatCommandDetail> BuildChatCommands();
    }
}
