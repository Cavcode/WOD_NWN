using System.Collections.Generic;

namespace WOD.Game.Server.Service.QuestService
{
    public interface IQuestListDefinition
    {
        public Dictionary<string, QuestDetail> BuildQuests();
    }
}
