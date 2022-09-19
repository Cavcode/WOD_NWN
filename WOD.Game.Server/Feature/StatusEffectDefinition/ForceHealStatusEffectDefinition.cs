﻿using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.SkillService;
using WOD.Game.Server.Service.StatusEffectService;

namespace WOD.Game.Server.Feature.StatusEffectDefinition
{
    public class ForceHealStatusEffectDefinition : IStatusEffectListDefinition
    {
        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            var builder = new StatusEffectBuilder();
            ForceHeal1(builder);
            ForceHeal2(builder);
            ForceHeal3(builder);
            ForceHeal4(builder);
            ForceHeal5(builder);

            return builder.Build();
        }

        private void ApplyHeal(uint source, uint target, int amount)
        {
            var wilBonus = GetAbilityModifier(AbilityType.Willpower, source) * 2;
            if (wilBonus < 0)
                wilBonus = 0;

            amount += wilBonus;

            ApplyEffectToObject(DurationType.Instant, GetRacialType(target) == RacialType.Undead
                ? EffectDamage(amount)
                : EffectHeal(amount), target);

            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Healing_S), target);

            Enmity.ModifyEnmityOnAll(source, 120 + amount);
            CombatPoint.AddCombatPointToAllTagged(source, SkillType.Force, 3);
        }

        private void ForceHeal1(StatusEffectBuilder builder)
        {
            const int Amount = 10;
            builder.Create(StatusEffectType.ForceHeal1)
                .Name("Force Heal I")
                .EffectIcon(EffectIconType.Regenerate)
                .CannotReplace(StatusEffectType.ForceHeal2, StatusEffectType.ForceHeal3, StatusEffectType.ForceHeal4, StatusEffectType.ForceHeal5)
                .GrantAction((source, target, length, data) =>
                {
                    ApplyHeal(source, target, Amount);
                })
                .TickAction((source, target, effectData) =>
                {
                    ApplyHeal(source, target, Amount);
                });
        }
        private void ForceHeal2(StatusEffectBuilder builder)
        {
            const int Amount = 15;
            builder.Create(StatusEffectType.ForceHeal2)
                .Name("Force Heal II")
                .EffectIcon(EffectIconType.Regenerate)
                .Replaces(StatusEffectType.ForceHeal1)
                .CannotReplace(StatusEffectType.ForceHeal3, StatusEffectType.ForceHeal4, StatusEffectType.ForceHeal5)
                .GrantAction((source, target, length, data) =>
                {
                    ApplyHeal(source, target, Amount);
                })
                .TickAction((source, target, effectData) =>
                {
                    ApplyHeal(source, target, Amount);
                });
        }
        private void ForceHeal3(StatusEffectBuilder builder)
        {
            const int Amount = 20;
            builder.Create(StatusEffectType.ForceHeal3)
                .Name("Force Heal III")
                .EffectIcon(EffectIconType.Regenerate)
                .Replaces(StatusEffectType.ForceHeal1, StatusEffectType.ForceHeal2)
                .CannotReplace(StatusEffectType.ForceHeal4, StatusEffectType.ForceHeal5)
                .GrantAction((source, target, length, data) =>
                {
                    ApplyHeal(source, target, Amount);
                })
                .TickAction((source, target, effectData) =>
                {
                    ApplyHeal(source, target, Amount);
                });
        }
        private void ForceHeal4(StatusEffectBuilder builder)
        {
            const int Amount = 25;
            builder.Create(StatusEffectType.ForceHeal4)
                .Name("Force Heal IV")
                .EffectIcon(EffectIconType.Regenerate)
                .Replaces(StatusEffectType.ForceHeal1, StatusEffectType.ForceHeal2, StatusEffectType.ForceHeal3)
                .CannotReplace(StatusEffectType.ForceHeal5)
                .GrantAction((source, target, length, data) =>
                {
                    ApplyHeal(source, target, Amount);
                })
                .TickAction((source, target, effectData) =>
                {
                    ApplyHeal(source, target, Amount);
                });
        }
        private void ForceHeal5(StatusEffectBuilder builder)
        {
            const int Amount = 30;
            builder.Create(StatusEffectType.ForceHeal5)
                .Name("Force Heal V")
                .EffectIcon(EffectIconType.Regenerate)
                .Replaces(StatusEffectType.ForceHeal1, StatusEffectType.ForceHeal2, StatusEffectType.ForceHeal3, StatusEffectType.ForceHeal4)
                .GrantAction((source, target, length, data) =>
                {
                    ApplyHeal(source, target, Amount);
                })
                .TickAction((source, target, effectData) =>
                {
                    ApplyHeal(source, target, Amount);
                });
        }
    }
}