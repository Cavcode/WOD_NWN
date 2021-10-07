using System;
using System.Collections.Generic;
using System.Reflection;
using WOD.Game.Server.Core;

namespace WOD.Game.Server.Service.GuiService
{
    public interface IGuiWidget
    {
        /// <summary>
        /// Retrieves the Id of the Widget.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Retrieves the list of elements this widget contains.
        /// </summary>
        public List<IGuiWidget> Elements { get; }

        /// <summary>
        /// Retrieves the set of events registered for this widget.
        /// </summary>
        public Dictionary<string, MethodInfo> Events { get; }

        /// <summary>
        /// Builds the widget element.
        /// </summary>
        /// <returns>Json representing the widget element.</returns>
        Json ToJson();
    }
}
