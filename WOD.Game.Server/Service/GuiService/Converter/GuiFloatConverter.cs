using WOD.Game.Server.Core;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.GuiService.Converter
{
    public class GuiFloatConverter : IGuiBindConverter<float>
    {
        public float ToObject(Json json)
        {
            return JsonGetFloat(json);
        }

        public Json ToJson(float obj)
        {
            return JsonFloat(obj);
        }
    }
}
