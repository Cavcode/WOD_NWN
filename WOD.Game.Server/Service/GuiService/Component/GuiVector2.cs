using WOD.Game.Server.Core;
using WOD.Game.Server.Core.Beamdog;

namespace WOD.Game.Server.Service.GuiService.Component
{
    public class GuiVector2
    {
        public float X { get; set; }
        public float Y { get; set; }

        public GuiVector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Json ToJson()
        {
            return Nui.Vec(X, Y);
        }
    }
}
