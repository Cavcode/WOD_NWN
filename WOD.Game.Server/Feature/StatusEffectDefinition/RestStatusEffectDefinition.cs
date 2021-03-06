using System;
using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.StatusEffectService;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature.StatusEffectDefinition
{
    public class RestStatusEffectDefinition: IStatusEffectListDefinition
    {
        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            var builder = new StatusEffectBuilder();
            Rest(builder);

            return builder.Build();
        }

        private void Rest(StatusEffectBuilder builder)
        {
            builder.Create(StatusEffectType.Rest)
                .Name("Rest")
                .EffectIcon(8) // 8 = Fatigue
                .GrantAction((source, target, length) =>
                {
                    // Store position the player is at when the rest effect is granted.
                    var position = GetPosition(target);

                    SetLocalFloat(target, "REST_POSITION_X", position.X);
                    SetLocalFloat(target, "REST_POSITION_Y", position.Y);
                    SetLocalFloat(target, "REST_POSITION_Z", position.Z);
                })
                .TickAction((source, target) =>
                {
                    var position = GetPosition(target);

                    var originalPosition = Vector3(
                        GetLocalFloat(target, "REST_POSITION_X"),
                        GetLocalFloat(target, "REST_POSITION_Y"),
                        GetLocalFloat(target, "REST_POSITION_Z"));

                    // Player has moved since the effect started. Remove it.
                    if(Math.Abs(position.X - originalPosition.X) > 0.1f ||
                       Math.Abs(position.Y - originalPosition.Y) > 0.1f ||
                       Math.Abs(position.Z - originalPosition.Z) > 0.1f)
                    {
                        StatusEffect.Remove(target, StatusEffectType.Rest);
                        return;
                    }

                    var hpAmount = 1 + GetAbilityModifier(AbilityType.Vitality, target) / 2;
                    var stmAmount = 1 + GetAbilityModifier(AbilityType.Perception, target) / 2;
                    var fpAmount = 1 + GetAbilityModifier(AbilityType.Social, target) / 2;

                    ApplyEffectToObject(DurationType.Instant, EffectHeal(hpAmount), target);
                    Stat.RestoreStamina(target, stmAmount);
                    Stat.RestoreFP(target, fpAmount);
                })
                .RemoveAction(target =>
                {
                    // Clean up position information.
                    DeleteLocalFloat(target, "REST_POSITION_X");
                    DeleteLocalFloat(target, "REST_POSITION_Y");
                    DeleteLocalFloat(target, "REST_POSITION_Z");
                });
        }
    }
}
