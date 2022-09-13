using WOD.Game.Server.Service.GuiService;

namespace WOD.Game.Server.Feature.GuiDefinition.Payload
{
    public class RenameItemPayload: GuiPayloadBase
    {
        public uint Target { get; set; }
        public RenameItemPayload(uint target)
        {
            Target = target;
        }
    }
}
