using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;

namespace WOD.Game.Server.Service.AnimationService
{
    public interface IAnimator
    {
        VisualEffect Vfx { get; set; }

        AnimationEvent Event { get; set; }

        DurationType Duration { get; set; }

        public float Scale { get; set; }

        public void SetLocalVariables(uint oObject);
    }
}
