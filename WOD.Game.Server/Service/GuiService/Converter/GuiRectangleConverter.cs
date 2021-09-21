using WOD.Game.Server.Core;
using WOD.Game.Server.Core.Beamdog;
using WOD.Game.Server.Service.GuiService.Component;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.GuiService.Converter
{
    public class GuiRectangleConverter : IGuiBindConverter<GuiRectangle>
    {
        public GuiRectangle ToObject(Json json)
        {
            var rect = new GuiRectangle(
                JsonGetFloat(JsonObjectGet(json, "x")),
                JsonGetFloat(JsonObjectGet(json, "y")),
                JsonGetFloat(JsonObjectGet(json, "w")),
                JsonGetFloat(JsonObjectGet(json, "h"))
            );

            return rect;
        }

        public Json ToJson(GuiRectangle obj)
        {
            return Nui.Rect(obj.X, obj.Y, obj.Width, obj.Height);
        }
    }
}
