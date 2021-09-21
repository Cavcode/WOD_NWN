using WOD.Game.Server.Core;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.GuiService.Converter
{
    public class GuiStringConverter: IGuiBindConverter<string>
    {
        public string ToObject(Json json)
        {
            return JsonGetString(json);
        }

        public Json ToJson(string obj)
        {
            return obj.ToJson();
        }
    }
}
