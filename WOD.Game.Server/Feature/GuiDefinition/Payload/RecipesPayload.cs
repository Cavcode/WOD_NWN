using WOD.Game.Server.Service.GuiService;
using WOD.Game.Server.Service.SkillService;

namespace WOD.Game.Server.Feature.GuiDefinition.Payload
{
    public class RecipesPayload: GuiPayloadBase
    {
        public SkillType Skill { get; set; }

        public RecipesPayload(SkillType skill)
        {
            Skill = skill;
        }
    }
}
