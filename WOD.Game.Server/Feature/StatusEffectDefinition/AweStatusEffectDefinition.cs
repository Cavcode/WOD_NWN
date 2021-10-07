using System;
using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.SkillService;
using WOD.Game.Server.Service.StatusEffectService;
using static WOD.Game.Server.Feature.DurationAudio;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature.StatusEffectDefinition
{
    public class AweStatusEffectDefinition : IStatusEffectListDefinition
    {
        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            var builder = new StatusEffectBuilder();
            Awe(builder);

            return builder.Build();
        }
        private void Awe(StatusEffectBuilder builder)
        {
            builder.Create(StatusEffectType.Awe)
               .Name("Awe")
                .EffectIcon(8) // Change this
                .GrantAction((source, target, length) =>
                {
                    // Play duration effect and activation sound if available.
                    var durationSound = EffectVisualEffect(VisualEffect.Awe_Duration);
                    DurationAudio.DurationAudioOn(source, "aweDur", durationSound);
                    PlaySound("aweAct");
                    if (!GetIsEnemy(target))
                    {
                        var effect = EffectAbilityIncrease(AbilityType.Might, 2);
                        effect = EffectAbilityIncrease(AbilityType.Perception, 2);
                        effect = EffectAbilityIncrease(AbilityType.Willpower, 2);
                        effect = EffectLinkEffects(effect, EffectVisualEffect(VisualEffect.Vfx_Dur_Iounstone_Blue));
                        effect = TagEffect(effect, "StatusEffectType." + StatusEffectType.Awe);
                        ApplyEffectToObject(DurationType.Permanent, effect, target);
                    }
                    else if (!Ability.GetAbilityResisted(source, target))
                    {
                        var effect = EffectAbilityDecrease(AbilityType.Might, 2);
                        effect = EffectAbilityDecrease(AbilityType.Perception, 2);
                        effect = EffectAbilityDecrease(AbilityType.Willpower, 2);
                        effect = EffectAttackDecrease(5);
                        effect = EffectLinkEffects(effect, EffectVisualEffect(VisualEffect.Vfx_Dur_Iounstone_Blue));
                        effect = TagEffect(effect, "StatusEffectType." + StatusEffectType.Awe);
                        ApplyEffectToObject(DurationType.Permanent, effect, target);
                    }
                    CombatPoint.AddCombatPointToAllTagged(target, SkillType.Presence, 3);
                })
                .RemoveAction((target) =>
                {
                    RemoveEffectByTag(target, "StatusEffectType." + StatusEffectType.Awe);

                })
                .RemoveAction((source) =>
                {
                    RemoveEffectByTag(source, "aweDur");
                });
        }
    }
}
