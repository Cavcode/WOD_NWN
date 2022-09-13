using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.PerkService;

namespace WOD.Game.Server.Feature.AbilityDefinition.TwoHanded
{
    public class StrongStyleSaberstaffAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            StrongStyleSaberstaff();

            return _builder.Build();
        }

        private void DoToggle(uint activator, AbilityToggleType type)
        {
            var isToggled = !Ability.IsAbilityToggled(activator, type);
            Ability.ToggleAbility(activator, type, isToggled);

            if (isToggled)
            {
                SendMessageToPC(activator, ColorToken.Green("Strong Style (Saberstaff) enabled"));
            }
            else
            {
                SendMessageToPC(activator, ColorToken.Red("Strong Style (Saberstaff) disabled"));
            }
        }

        private void StrongStyleSaberstaff()
        {
            _builder.Create(FeatType.StrongStyleSaberstaff, PerkType.StrongStyleSaberstaff)
                .Name("Strong Style (Saberstaff)")
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HideActivationMessage()
                .HasImpactAction((activator, target, level, location) =>
                {
                    DoToggle(activator, AbilityToggleType.StrongStyleSaberstaff);
                });
        }
    }
}
