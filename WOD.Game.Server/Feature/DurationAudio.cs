using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.Item;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature
{
    public static class DurationAudio
    {
        /// <summary>
        /// Play a duration sound for an ability.
        /// </summary>
        public static void DurationAudioOn(uint activator, string tag, Effect effect)
        {
            effect = TagEffect(effect, tag);

            ApplyEffectToObject(DurationType.Permanent, effect, activator);
            AssignCommand(activator, () => PlaySound(tag));
        }

    }
}
