using System;
using System.Collections.Generic;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.Beamdog;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.GuiService.Component
{
    public class GuiColumn<T> : GuiWidget<T, GuiColumn<T>>
        where T: IGuiViewModel
    {
        public List<GuiRow<T>> Rows { get; }

        public GuiColumn<T> AddRow(Action<GuiRow<T>> row)
        {
            var newRow = new GuiRow<T>();
            Rows.Add(newRow);
            row(newRow);

            return this;
        }

        public GuiColumn()
        {
            Rows = new List<GuiRow<T>>();
        }

        public override Json BuildElement()
        {
            var column = JsonArray();

            foreach (var row in Rows)
            {
                column = JsonArrayInsert(column, row.ToJson());
            }

            return Nui.Column(column);
        }
    }
}
