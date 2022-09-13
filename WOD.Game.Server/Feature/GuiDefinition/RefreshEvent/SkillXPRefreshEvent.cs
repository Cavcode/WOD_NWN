using System.Collections.Generic;
using WOD.Game.Server.Service.GuiService;
using WOD.Game.Server.Service.SkillService;

namespace WOD.Game.Server.Feature.GuiDefinition.RefreshEvent
{
    public class SkillXPRefreshEvent : IGuiRefreshEvent
    {
        public List<SkillType> ModifiedSkills { get; set; }

        public SkillXPRefreshEvent(List<SkillType> skillTypes)
        {
            ModifiedSkills = skillTypes;
        }
    }
}
