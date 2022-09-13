﻿using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.StatusEffectService;

namespace WOD.Game.Server.Feature.AbilityDefinition.Force
{
    public class PremonitionAbilityDefinition: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Premonition1();
            Premonition2();

            return _builder.Build();
        }

        private void Premonition1()
        {
            _builder.Create(FeatType.Premonition1, PerkType.Premonition)
                .Name("Premonition I")
                .Level(1)
                .HasRecastDelay(RecastGroup.Premonition, 60f)
                .RequirementFP(4)
                .IsCastedAbility()
                .IsConcentrationAbility(StatusEffectType.Premonition1)
                .UsesAnimation(Animation.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating();
        }

        private void Premonition2()
        {
            _builder.Create(FeatType.Premonition2, PerkType.Premonition)
                .Name("Premonition II")
                .Level(2)
                .HasRecastDelay(RecastGroup.Premonition, 60f)
                .RequirementFP(6)
                .IsCastedAbility()
                .IsConcentrationAbility(StatusEffectType.Premonition2)
                .UsesAnimation(Animation.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating();
        }
    }
}
