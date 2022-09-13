using System.Collections.Generic;
using WOD.Game.Server.Service.SkillService;

namespace WOD.Game.Server.Service.LanguageService
{
    public class LanguageCommand
    {
        public string ProperName { get; }
        public IEnumerable<string> ChatNames { get; }
        public SkillType Skill { get; }

        public LanguageCommand(string properName, SkillType skill, IEnumerable<string> chatNames)
        {
            ProperName = properName;
            Skill = skill;
            ChatNames = chatNames;
        }
    }
}
