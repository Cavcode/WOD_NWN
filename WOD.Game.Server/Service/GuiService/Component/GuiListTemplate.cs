﻿using System;
using System.Collections.Generic;
using WOD.Game.Server.Core;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.GuiService.Component
{
    public class GuiListTemplate<T>
        where T: IGuiViewModel
    {
        private List<GuiListTemplateCell<T>> Cells { get; set; }

        public GuiListTemplate()
        {
            Cells = new List<GuiListTemplateCell<T>>();
        }

        public GuiListTemplate<T> AddCell(Action<GuiListTemplateCell<T>> cell)
        {
            var newCell = new GuiListTemplateCell<T>();
            Cells.Add(newCell);
            cell(newCell);

            return this;
        }

        public Json ToJson()
        {
            var template = JsonArray();

            foreach (var cell in Cells)
            {
                template = JsonArrayInsert(template, cell.ToJson());
            }

            return template;
        }
    }
}
