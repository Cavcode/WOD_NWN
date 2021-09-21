using WOD.Game.Server.Core;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.GuiService.Converter
{
    public class GuiIntConverter: IGuiBindConverter<int>
    {
        public int ToObject(Json json)
        {
            return JsonGetInt(json);
        }

        public Json ToJson(int obj)
        {
            return JsonInt(obj);
        }
    }
}
