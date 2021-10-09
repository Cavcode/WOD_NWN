using System;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.GuiService;
using WOD.Game.Server.Service.GuiService.Component;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Service.CombatService;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature.GuiDefinition.ViewModel
{
    public class FeedBarViewModel : GuiViewModelBase<FeedBarViewModel>
    {
        public float Progress
        {
            get => Get<float>();
            set => Set(value);
        }


        public Action OnLoadWindow() => () =>
        {
            StartFeed();
        };

        private void StartFeed()
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);
            var playerType = GetClassByPosition(1, Player);
            var target = GetSpellTargetObject();
            var fpAmount = 5;
            var targetBloodAmount = GetLocalInt(target,"bloodAmount");

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
            targetBloodAmount -= 5;
            Stat.RestoreFP(Player, fpAmount);
            SetLocalInt(target, "bloodAmount", targetBloodAmount);

            // Check if target blood is 0. Kill them if it is.
            if (targetBloodAmount <= 0)
            {
                var death = EffectDeath();
                ApplyEffectToObject(DurationType.Instant, death, target);
            }
        }
    }
}
