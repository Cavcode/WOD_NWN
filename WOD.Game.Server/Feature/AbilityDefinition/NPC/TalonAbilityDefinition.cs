using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.CombatService;
using WOD.Game.Server.Service.PerkService;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature.AbilityDefinition.NPC
{
    public class TalonAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new AbilityBuilder();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Talon();

            return _builder.Build();
        }

        private void Talon()
        {
            _builder.Create(FeatType.Talon, PerkType.Invalid)
                .Name("Talon")
                .HasActivationDelay(2.0f)
                .HasRecastDelay(RecastGroup.Talon, 40f)
                .IsCastedAbility()
                .RequirementStamina(3)
                .HasImpactAction((activator, target, level, location) =>
                {
                    var might = GetAbilityModifier(AbilityType.Might, activator);
                    var dmg = 1.0f;
                    var defense = Stat.GetDefense(target, CombatDamageType.Physical);
                    var vitality = GetAbilityModifier(AbilityType.Vitality, target);
                    var damage = Combat.CalculateDamage(dmg, might, defense, vitality, false);

                    ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Piercing), target);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Com_Blood_Spark_Medium), target);
                });
        }
    }
}
