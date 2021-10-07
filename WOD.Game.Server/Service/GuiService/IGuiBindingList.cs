using System.ComponentModel;

namespace WOD.Game.Server.Service.GuiService
{
    public interface IGuiBindingList
    {
        string PropertyName { get; set; }
        event ListChangedEventHandler ListChanged;
        int Count { get; }
    }
}
