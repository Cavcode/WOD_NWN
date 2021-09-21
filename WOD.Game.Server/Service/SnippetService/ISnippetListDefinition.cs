using System.Collections.Generic;

namespace WOD.Game.Server.Service.SnippetService
{
    public interface ISnippetListDefinition
    {
        public Dictionary<string, SnippetDetail> BuildSnippets();
    }
}
