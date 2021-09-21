using WOD.Game.Server.Core;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.GuiService
{
    public static class GuiJsonExtensions
    {
        public static Json ToJson(this string @string)
        {
            return JsonString(@string);
        }
        public static Json ToJson(this int @int)
        {
            return JsonInt(@int);
        }
        public static Json ToJson(this float @float)
        {
            return JsonFloat(@float);
        }

        public static Json ToJson(this bool @bool)
        {
            return JsonBool(@bool);
        }
    }
}
