using WOD.Game.Server.Core;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.GuiService.Converter
{
    public class GuiBoolConverter: IGuiBindConverter<bool>
    {
        public bool ToObject(Json json)
        {
            return JsonGetInt(json) == 1;
        }

        public Json ToJson(bool obj)
        {
            return JsonBool(obj);
        }
    }
}
