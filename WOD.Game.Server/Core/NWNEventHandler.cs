using System;

namespace WOD.Game.Server.Core
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class NWNEventHandler : Attribute
    {
        public string Script { get; }

        public NWNEventHandler(string script)
        {
            Script = script;
        }

    }
}