﻿using WOD.Game.Server.Core;
using WOD.Game.Server.Core.Beamdog;
using WOD.Game.Server.Service.GuiService.Component;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.GuiService.Converter
{
    public class GuiColorConverter: IGuiBindConverter<GuiColor>
    {
        public GuiColor ToObject(Json json)
        {
            var color = new GuiColor(
                JsonGetInt(JsonObjectGet(json, "r")),
                JsonGetInt(JsonObjectGet(json, "g")),
                JsonGetInt(JsonObjectGet(json, "b")),
                JsonGetInt(JsonObjectGet(json, "a"))
            );

            return color;
        }

        public Json ToJson(GuiColor obj)
        {
            return Nui.Color(obj.Red, obj.Green, obj.Blue, obj.Alpha);
        }
    }
}
