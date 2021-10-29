using System;
using WOD.Game.Server.Core.NWNX;

namespace WOD.Game.Server.Core
{
    public class Profiler : IDisposable
    {
        public Profiler(string name)
        {
            //ProfilerPlugin.PushPerfScope(name, "RunScript", "Script");
        }

        public void Dispose()
        {
            //ProfilerPlugin.PopPerfScope();
        }
    }
}
