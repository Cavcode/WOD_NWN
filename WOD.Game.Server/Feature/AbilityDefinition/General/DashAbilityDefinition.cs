using System.Collections.Generic;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.PerkService;

namespace WOD.Game.Server.Feature.AbilityDefinition.General
{
    public class DashAbilityDefinition: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Dash();

            return _builder.Build();
        }

        [NWNEventHandler("space_enter")]
        public static void EnterSpace()
        {
            var player = OBJECT_SELF;
            Ability.ToggleAbility(player, AbilityToggleType.Dash, false);
        }

        private void Dash()
        {
            _builder.Create(FeatType.Dash, PerkType.Dash)
                .Name("Dash")
                .HideActivationMessage()
                .UnaffectedByHeavyArmor()
                .HasImpactAction((activator, target, level, location) =>
                {
                    var toggle = !Ability.IsAbilityToggled(target, AbilityToggleType.Dash);
                    Ability.ToggleAbility(target, AbilityToggleType.Dash, toggle);
                });
        }
    }
}
