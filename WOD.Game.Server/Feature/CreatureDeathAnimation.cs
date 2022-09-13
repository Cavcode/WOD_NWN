using WOD.Game.Server.Core;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AnimationService;

namespace WOD.Game.Server.Feature
{
    public static class CreatureDeathAnimation
    {
        [NWNEventHandler("crea_death_aft")]
        public static void OnDeath()
        {
            var creature = OBJECT_SELF;
            AnimationPlayer.Play(creature, AnimationEvent.CreatureOnDeath);
        }
    }
}
