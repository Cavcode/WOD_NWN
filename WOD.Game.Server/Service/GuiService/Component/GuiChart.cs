﻿using System;
using System.Collections.Generic;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.Beamdog;

namespace WOD.Game.Server.Service.GuiService.Component
{
    public class GuiChart<T> : GuiWidget<T, GuiChart<T>>
        where T: IGuiViewModel
    {
        private List<GuiChartSlot<T>> Slots { get; set; }

        /// <summary>
        /// Adds a slot to the chart.
        /// </summary>
        /// <param name="slot">The new slot</param>
        public GuiChart<T> AddSlot(Action<GuiChartSlot<T>> slot)
        {
            var newSlot = new GuiChartSlot<T>();
            Slots.Add(newSlot);
            slot(newSlot);

            return this;
        }

        public GuiChart()
        {
            Slots = new List<GuiChartSlot<T>>();
        }
        
        /// <summary>
        /// Builds the GuiChart element.
        /// </summary>
        /// <returns>Json representing the chart element.</returns>
        public override Json BuildElement()
        {
            var slots = JsonArray();

            foreach (var slot in Slots)
            {
                slots = JsonArrayInsert(slots, slot.ToJson());
            }

            return Nui.Chart(slots);
        }
    }
}
