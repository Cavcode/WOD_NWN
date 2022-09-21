using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.StatusEffectService;
using WOD.Game.Server.Enumeration;

namespace WOD.Game.Server.Feature
{
    public static class NaturalRegeneration
    {
        /// <summary>
        /// On module heartbeat, process a player's HP/FP/STM regeneration.
        /// </summary>
        [NWNEventHandler("interval_pc_6s")]
        public static void ProcessRegeneration()
        {
            var player = OBJECT_SELF;
            if (!GetIsPC(player) || GetIsDM(player)) return;
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);

            if (dbPlayer.CharacterType == CharacterType.Kindred) return;

            var tick = GetLocalInt(player, "NATURAL_REGENERATION_TICK") + 1;
            if (tick >= 5) // 6 seconds * 5 = 30 seconds
            {
                var vitalityBonus = GetAbilityModifier(AbilityType.Vitality, player);
                if (vitalityBonus < 0)
                    vitalityBonus = 0;

                var hpRegen = dbPlayer.HPRegen + vitalityBonus * 4;


                if (hpRegen > 0 && GetCurrentHitPoints(player) < GetMaxHitPoints(player))
                {
                    ApplyEffectToObject(DurationType.Instant, EffectHeal(hpRegen), player);
                }

                //if (fpRegen > 0)
                //{
                //    Stat.RestoreResource(player, fpRegen, dbPlayer);
                //}

                tick = 0;
            }

            //SetLocalInt(player, "NATURAL_REGENERATION_TICK", tick);
        }
    }
}
