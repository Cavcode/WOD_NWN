using System.ComponentModel;

namespace WOD.Game.Server.Service.GuiService
{
    public class GuiBindingList<T> : BindingList<T>, IGuiBindingList
    {
        public string PropertyName { get; set; }
    }
}
