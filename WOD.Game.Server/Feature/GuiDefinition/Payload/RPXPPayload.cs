using WOD.Game.Server.Service.GuiService;
using WOD.Game.Server.Service.SkillService;

namespace WOD.Game.Server.Feature.GuiDefinition.Payload
{
    public class RPXPPayload: GuiPayloadBase
    {
        public string SkillName { get; set; }
        public int MaxRPXP { get; set; }
        public SkillType Skill { get; set; }
    }
}
