using WOD.Game.Server.Core;

namespace WOD.Game.Server.Service.GuiService.Converter
{
    public interface IGuiBindConverter<T>
    {
        T ToObject(Json json);
        Json ToJson(T obj);
    }
}
