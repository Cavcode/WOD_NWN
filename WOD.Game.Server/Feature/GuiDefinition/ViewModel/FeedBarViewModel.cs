using System;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.GuiService;
using WOD.Game.Server.Service.GuiService.Component;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Service.CombatService;
using WOD.Game.Server.Core;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature.GuiDefinition.ViewModel
{
    public class FeedBarViewModel: GuiViewModelBase<FeedBarViewModel, GuiPayloadBase>
    {
        public float Progress
        {
            get => Get<float>();
            set => Set(value);
        }


        private void StartFeed()
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);
            var playerType = GetClassByPosition(1, Player);
            var target = GetLocalObject(Player, "bloodTarget");
            var targetBloodAmount = GetLocalFloat(target,"bloodAmount");
            var progress = 0.0f;

            // Check if target blood is 0. Kill them if it is.
            if (targetBloodAmount <= 0)
            {
                var death = EffectDeath();
                ApplyEffectToObject(DurationType.Instant, death, target);
                FloatingTextStringOnCreature("You have drained your target to death.", Player);
  
                Gui.TogglePlayerWindow(Player, GuiWindowType.FeedBar);
                Effect eLoop = GetFirstEffect(Player);

                while (GetIsEffectValid(eLoop))
                {
                    if (GetEffectType(eLoop) == EffectTypeScript.Blindness)
                        RemoveEffect(Player, eLoop);

                    eLoop = GetNextEffect(Player);
                }

                Scheduler.Unschedule(Scheduler.GetSchedule("feedSchedule"));
            }

            if (GetLocalFloat(Player, "targetBloodAmountMax") == 0.0f)
            {
                SetLocalFloat(Player, "targetBloodAmountMax", targetBloodAmount);
                progress = 100.0f;
                SetLocalFloat(Player, "feedProgress", 1.0f);
            }
            var totalBloodAmount = GetLocalFloat(Player, "targetBloodAmountMax");
            progress = GetLocalFloat(Player, "feedProgress") - 0.1f;
            Progress = progress;
            SetLocalFloat(Player, "feedProgress", progress);

            SendMessageToPC(Player, "we're in feedbar model");
            SendMessageToPC(Player,"Target:" + GetName(target));
            SendMessageToPC(Player, "Progress: " + FloatToString(progress));

            // Ventrue will vomit if they drink rat or lesser blood.
            if (playerType == ClassType.Ventrue && GetLocalInt(target,"enemyType") == 1)
            {
                // Stun player and play vomit animation. Gross.
                var daze = EffectDazed();
                ApplyEffectToObject(DurationType.Temporary, daze, target, 3.0f);
                Gui.TogglePlayerWindow(Player,GuiWindowType.FeedBar);
                return;
            }     

            // Proceed with blood drinking.
            targetBloodAmount -= (0.1f * GetLocalFloat(Player, "targetBloodAmountMax"));
            Stat.RestoreFP(Player, FloatToInt(targetBloodAmount));
            SetLocalFloat(target, "bloodAmount", targetBloodAmount);
        }

        protected override void Initialize(GuiPayloadBase initialPayload)
        {
            StartFeed();
            AssignCommand(Player, () =>
            {
                ClearAllActions();
                PlaySound("feed_on_start");
                PlaySound("heartbeat_loop");
                PlaySound("feed_on_loop");
            });
            Scheduler.ScheduleRepeating(StartFeed, TimeSpan.FromSeconds(1), "feedSchedule");
        }
    }
}
