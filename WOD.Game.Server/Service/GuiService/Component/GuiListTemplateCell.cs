using WOD.Game.Server.Core;
using WOD.Game.Server.Core.Beamdog;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.GuiService.Component
{
    public class GuiListTemplateCell<T> : GuiExpandableComponent<T>
        where T: IGuiViewModel
    {
        private float CellWidth { get; set; }
        private bool IsStatic { get; set; }

        public GuiListTemplateCell<T> SetCellWidth(float cellWidth)
        {
            CellWidth = cellWidth;
            return this;
        }

        public GuiListTemplateCell<T> SetIsStatic(bool isStatic)
        {
            IsStatic = isStatic;
            return this;
        }

        public GuiListTemplateCell()
        {
            CellWidth = 0f;
            IsStatic = false;
        }

        public override Json BuildElement()
        {
            var elements = JsonArray();

            foreach (var element in Elements)
            {
                elements = JsonArrayInsert(elements, element.ToJson());
            }

            return Nui.ListTemplateCell(elements, CellWidth, IsStatic);
        }
    }
}
