﻿using WOD.Game.Server.Core;
using WOD.Game.Server.Core.Beamdog;

namespace WOD.Game.Server.Service.GuiService.Component
{
    public class GuiColor
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public int Alpha { get; set; }

        public GuiColor(int red, int green, int blue, int alpha = 255)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public Json ToJson()
        {
            return Nui.Color(Red, Green, Blue, Alpha);
        }
    }
}
