using System;
using System.Linq.Expressions;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.Beamdog;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.GuiService.Component
{
    public class GuiList<T> : GuiWidget<T, GuiList<T>>
        where T: IGuiViewModel
    {
        private GuiListTemplate<T> Template { get; set; }

        private int RowCount { get; set; }
        private string RowCountBindName { get; set; }
        private bool IsRowCountBound => !string.IsNullOrWhiteSpace(RowCountBindName);

        private float RowHeight { get; set; }
        
        /// <summary>
        /// Sets a static value for the row count.
        /// </summary>
        /// <param name="rowCount">The number of rows</param>
        public GuiList<T> SetRowCount(int rowCount)
        {
            RowCount = rowCount;
            return this;
        }

        /// <summary>
        /// Binds a dynamic value for the row count.
        /// </summary>
        /// <typeparam name="TProperty">The property of the view model.</typeparam>
        /// <param name="expression">Expression to target the property.</param>
        public GuiList<T> BindRowCount<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            RowCountBindName = GuiHelper<T>.GetPropertyName(expression) + "_RowCount";
            return this;
        }

        /// <summary>
        /// Sets a static value for the height of the rows within the list.
        /// </summary>
        /// <param name="rowHeight">The height of the rows</param>
        public GuiList<T> SetRowHeight(float rowHeight)
        {
            RowHeight = rowHeight;
            return this;
        }

        public GuiList(GuiListTemplate<T> template)
        {
            Template = template;
            RowHeight = NuiStyle.RowHeight;

            Elements.Add(Template);
        }

        /// <summary>
        /// Builds the GuiList element.
        /// </summary>
        /// <returns>Json representing the list element.</returns>
        public override Json BuildElement()
        {
            var template = Template.ToJson();
            var rowCount = IsRowCountBound ? Nui.Bind(RowCountBindName) : JsonInt(RowCount);

            var json = Nui.List(template, rowCount, RowHeight);
            return json;
        }
    }
}
