using System;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.Beamdog;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.GuiService.Component
{
    public class GuiListTemplate<T>: GuiExpandableComponent<T>
        where T: IGuiViewModel
    {
        public override Json BuildElement()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Serializes the list template into Json.
        /// </summary>
        public override Json ToJson()
        {
            var template = JsonArray();

            foreach (var element in Elements)
            {
                var json = element.ToJson();
                template = JsonArrayInsert(template, Nui.ListTemplateCell(json, 0f, true));
            }

            return template;
        }
    }
}
