using WOD.Game.Server.Service.GuiService;

namespace WOD.Game.Server.Feature.GuiDefinition.Payload
{
    public class DMPlayerExaminePayload: GuiPayloadBase
    {
        public uint Target { get; set; }

        public DMPlayerExaminePayload(uint target)
        {
            Target = target;
        }
    }
}
