using System.Collections.Generic;
using WOD.Game.Server.Service.QuestService;

namespace WOD.Game.Server.Feature.QuestDefinition
{
    public class MonCalaQuestDefinition : IQuestListDefinition
    {
        public Dictionary<string, QuestDetail> BuildQuests()
        {
            var builder = new QuestBuilder();

            return builder.Build();
        }
    }
}