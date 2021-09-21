using WOD.Game.Server.Core;

namespace WOD.Game.Server.Service.GuiService
{
    public interface IGuiViewModel
    {
        void Bind(uint player, int windowToken);
        void UpdatePropertyFromClient(string propertyName);
    }
}
