//using Random = WOD.Game.Server.Service.Random;

using System.Collections.Generic;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.SkillService;
using WOD.Game.Server.Service.StatusEffectService;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature.AbilityDefinition.Disciplines.Presence
{
    public class AweAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            Awe(builder);

            return builder.Build();
        }

        private static void ImpactAction(uint activator, uint target, int level, Location targetLocation)
        {
            // If activator is in stealth mode, force them out of stealth mode.
            if (GetActionMode(activator, ActionMode.Stealth) == true)
                SetActionMode(activator, ActionMode.Stealth, false);
        
            var count = 0;
            var creature = GetFirstObjectInShape(Shape.Sphere, RadiusSize.Colossal, GetLocation(target), true, ObjectType.Creature);
            while (GetIsObjectValid(creature) && count < 6)
            {
            
                Enmity.ModifyEnmity(activator, creature, 30);
                StatusEffect.Apply(activator, creature, StatusEffectType.Awe, 16f);
                CombatPoint.AddCombatPoint(activator, creature, SkillType.Presence, 3);
                count++;
            
                creature = GetNextObjectInShape(Shape.Cone, RadiusSize.Colossal, GetLocation(target), true, ObjectType.Creature);
            }
        }

        private static void Awe(AbilityBuilder builder)
        {
            builder.Create(FeatType.Awe, PerkType.Awe)
                .Name("Awe")
                .RequirementFP(5)
                .HasImpactAction(ImpactAction)
                .HasRecastDelay(RecastGroup.Awe,4f);
        }

    }
}