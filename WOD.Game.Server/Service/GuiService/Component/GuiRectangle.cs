using WOD.Game.Server.Core;
using WOD.Game.Server.Core.Beamdog;

namespace WOD.Game.Server.Service.GuiService.Component
{
    public class GuiRectangle
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public GuiRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Json ToJson()
        {
            return Nui.Rect(X, Y, Width, Height);
        }
    }
}
