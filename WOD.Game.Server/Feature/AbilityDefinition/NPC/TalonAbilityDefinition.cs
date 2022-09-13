using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.CombatService;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.SkillService;

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
                    const int DMG = 1;
                    var attackerStat = GetAbilityScore(activator, AbilityType.Might);
                    var attack = Stat.GetAttack(activator, AbilityType.Might, SkillType.Invalid);
                    var defense = Stat.GetDefense(target, CombatDamageType.Physical, AbilityType.Vitality);
                    var defenderStat = GetAbilityScore(target, AbilityType.Vitality);
                    var damage = Combat.CalculateDamage(
                        attack,
                        DMG, 
                        attackerStat, 
                        defense, 
                        defenderStat, 
                        0);

                    ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Piercing), target);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Com_Blood_Spark_Medium), target);
                });
        }
    }
}
