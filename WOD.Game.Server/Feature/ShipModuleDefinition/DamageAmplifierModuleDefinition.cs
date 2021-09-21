﻿using System.Collections.Generic;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.SpaceService;

namespace WOD.Game.Server.Feature.ShipModuleDefinition
{
    public class DamageAmplifierModuleDefinition : IShipModuleListDefinition
    {
        private readonly ShipModuleBuilder _builder = new ShipModuleBuilder();

        public Dictionary<string, ShipModuleDetail> BuildShipModules()
        {
            EMDamageAmplifier("em_amp_b", "Basic EM Amplifier", 2, 1);
            EMDamageAmplifier("em_amp_1", "EM Amplifier I", 4, 2);
            EMDamageAmplifier("em_amp_2", "EM Amplifier II", 6, 3);
            EMDamageAmplifier("em_amp_3", "EM Amplifier III", 8, 4);
            EMDamageAmplifier("em_amp_4", "EM Amplifier IV", 10, 5);

            ThermalDamageAmplifier("therm_amp_b", "Basic Thermal Amplifier", 2, 1);
            ThermalDamageAmplifier("therm_amp_1", "Thermal Amplifier I", 4, 2);
            ThermalDamageAmplifier("therm_amp_2", "Thermal Amplifier II", 6, 3);
            ThermalDamageAmplifier("therm_amp_3", "Thermal Amplifier III", 8, 4);
            ThermalDamageAmplifier("therm_amp_4", "Thermal Amplifier IV", 10, 5);

            ExplosiveDamageAmplifier("exp_amp_b", "Basic Explosive Amplifier", 2, 1);
            ExplosiveDamageAmplifier("exp_amp_1", "Explosive Amplifier I", 4, 2);
            ExplosiveDamageAmplifier("exp_amp_2", "Explosive Amplifier II", 6, 3);
            ExplosiveDamageAmplifier("exp_amp_3", "Explosive Amplifier III", 8, 4);
            ExplosiveDamageAmplifier("exp_amp_4", "Explosive Amplifier IV", 10, 5);

            return _builder.Build();
        }

        private void EMDamageAmplifier(string itemTag, string name, int emDamageBonus, int requiredLevel)
        {
            _builder.Create(itemTag)
                .Name(name)
                .ShortName(name)
                .Texture("iit_ess_210")
                .Description($"Improves a ship's EM damage by {emDamageBonus}.")
                .PowerType(ShipModulePowerType.Low)
                .RequirePerk(PerkType.OffensiveModules, requiredLevel)
                .EquippedAction((creature, shipStatus) =>
                {
                    shipStatus.EMDamage += emDamageBonus;
                })
                .UnequippedAction((creature, shipStatus) =>
                {
                    shipStatus.EMDamage -= emDamageBonus;
                });
        }

        private void ThermalDamageAmplifier(string itemTag, string name, int thermalDamageBonus, int requiredLevel)
        {
            _builder.Create(itemTag)
                .Name(name)
                .ShortName(name)
                .Texture("iit_ess_214")
                .Description($"Improves a ship's thermal damage by {thermalDamageBonus}.")
                .PowerType(ShipModulePowerType.Low)
                .RequirePerk(PerkType.OffensiveModules, requiredLevel)
                .EquippedAction((creature, shipStatus) =>
                {
                    shipStatus.ThermalDamage += thermalDamageBonus;
                })
                .UnequippedAction((creature, shipStatus) =>
                {
                    shipStatus.ThermalDamage -= thermalDamageBonus;
                });
        }

        private void ExplosiveDamageAmplifier(string itemTag, string name, int explosiveDamageBonus, int requiredLevel)
        {
            _builder.Create(itemTag)
                .Name(name)
                .ShortName(name)
                .Texture("iit_ess_216")
                .Description($"Improves a ship's explosive damage by {explosiveDamageBonus}.")
                .PowerType(ShipModulePowerType.Low)
                .RequirePerk(PerkType.OffensiveModules, requiredLevel)
                .EquippedAction((creature, shipStatus) =>
                {
                    shipStatus.ExplosiveDamage += explosiveDamageBonus;
                })
                .UnequippedAction((creature, shipStatus) =>
                {
                    shipStatus.ExplosiveDamage -= explosiveDamageBonus;
                });
        }
    }
}