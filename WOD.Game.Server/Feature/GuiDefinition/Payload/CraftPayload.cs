using WOD.Game.Server.Service.CraftService;
using WOD.Game.Server.Service.GuiService;

namespace WOD.Game.Server.Feature.GuiDefinition.Payload
{
    public class CraftPayload: GuiPayloadBase
    {
        public RecipeType Recipe { get; set; }

        public CraftPayload(RecipeType recipe)
        {
            Recipe = recipe;
        }
    }
}
