﻿using System;
using System.Linq.Expressions;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.Beamdog;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.GuiService.Component
{
    public class GuiImage<T> : GuiWidget<T, GuiImage<T>>
        where T: IGuiViewModel
    {
        private string Resref { get; set; }
        private string ResrefBindName { get; set; }
        private bool IsResrefBound => !string.IsNullOrWhiteSpace(ResrefBindName);
        
        private NuiAspect Aspect { get; set; }
        private string AspectBindName { get; set; }
        private bool IsAspectBound => !string.IsNullOrWhiteSpace(AspectBindName);
        
        private NuiHorizontalAlign HorizontalAlign { get; set; }
        private string HorizontalAlignBindName { get; set; }
        private bool IsHorizontalAlignBound => !string.IsNullOrWhiteSpace(HorizontalAlignBindName);
        
        private NuiVerticalAlign  VerticalAlign { get; set; }
        private string VerticalAlignBindName { get; set; }
        private bool IsVerticalAlignBound => !string.IsNullOrWhiteSpace(VerticalAlignBindName);

        public GuiImage<T> SetResref(string resref)
        {
            Resref = resref;
            return this;
        }

        public GuiImage<T> BindResref<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            ResrefBindName = GuiHelper<T>.GetPropertyName(expression);
            return this;
        }

        public GuiImage<T> SetAspect(NuiAspect aspect)
        {
            Aspect = aspect;
            return this;
        }

        public GuiImage<T> BindAspect<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            AspectBindName = GuiHelper<T>.GetPropertyName(expression);
            return this;
        }

        public GuiImage<T> SetHorizontalAlign(NuiHorizontalAlign hAlign)
        {
            HorizontalAlign = hAlign;
            return this;
        }

        public GuiImage<T> BindHorizontalAlign<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            HorizontalAlignBindName = GuiHelper<T>.GetPropertyName(expression);
            return this;
        }

        public GuiImage<T> SetVerticalAlign(NuiVerticalAlign vAlign)
        {
            VerticalAlign = vAlign;
            return this;
        }

        public GuiImage<T> BindVerticalAlign<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            VerticalAlignBindName = GuiHelper<T>.GetPropertyName(expression);
            return this;
        }

        public override Json BuildElement()
        {
            var resref = IsResrefBound ? Nui.Bind(ResrefBindName) : JsonString(Resref);
            var aspect = IsAspectBound ? Nui.Bind(AspectBindName) : JsonInt((int) Aspect);
            var hAlign = IsHorizontalAlignBound ? Nui.Bind(HorizontalAlignBindName) : JsonInt((int) HorizontalAlign);
            var vAlign = IsVerticalAlignBound ? Nui.Bind(VerticalAlignBindName) : JsonInt((int) VerticalAlign);

            return Nui.Image(resref, aspect, hAlign, vAlign);
        }
    }
}
