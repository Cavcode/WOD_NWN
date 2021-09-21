using System;
using System.Linq.Expressions;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.Beamdog;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.GuiService.Component
{
    public class GuiProgressBar<T> : GuiWidget<T, GuiProgressBar<T>>
        where T: IGuiViewModel
    {
        private float Value { get; set; }
        private string ValueBindName { get; set; }
        private bool IsValueBound => !string.IsNullOrWhiteSpace(ValueBindName);

        public GuiProgressBar<T> SetValue(float value)
        {
            Value = value;
            return this;
        }

        public GuiProgressBar<T> BindValue<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            ValueBindName = GuiHelper<T>.GetPropertyName(expression);
            return this;
        }

        public override Json BuildElement()
        {
            var value = IsValueBound ? Nui.Bind(ValueBindName) : JsonFloat(Value);

            return Nui.Progress(value);
        }
    }
}
