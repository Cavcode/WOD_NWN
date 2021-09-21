using System;
using System.Collections.Generic;
using System.Reflection;
using WOD.Game.Server.Core;

namespace WOD.Game.Server.Service.GuiService
{
    public interface IGuiWidget
    {
        string Id { get; }
        public Dictionary<string, MethodInfo> Events { get; }
        Json ToJson();
    }
}
