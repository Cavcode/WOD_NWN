using System.Collections.Generic;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWNX;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.Item;
using WOD.Game.Server.Enumeration;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service
{
    public static class Combat
    {
        private static readonly Dictionary<int, float> _dmgValues = new Dictionary<int, float>();
        
        /// <summary>
        /// When the module loads, cache the DMG values found in iprp_dmg.2da into memory.
        /// </summary>
        [NWNEventHandler("mod_load")]
        public static void CacheData()
        {
            var rowCount = UtilPlugin.Get2DARowCount("iprp_dmg");

            for (var row = 0; row < rowCount; row++)
            {
                var label = Get2DAString("iprp_dmg", "Label", row);

                if (float.TryParse(label, out var dmgValue))
                {
                    _dmgValues[row] = dmgValue;
                }
            }
        }

        /// <summary>
        /// Retrieves the DMG value by a defense item property's cost table value.
        /// </summary>
        /// <param name="costTableValue">The cost table value of the defense item property.</param>
        /// <returns>The DMG value</returns>
        public static float GetDMGValueFromItemPropertyCostTableValue(int costTableValue)
        {
            if (!_dmgValues.ContainsKey(costTableValue))
                return 0.5f;

            return _dmgValues[costTableValue];
        }

        /// <summary>
        /// Calculates damage based on the damage formula:
        /// ((DMG+attackAttribute/3)*6)-((defense+defenseAttribute/3)/3)
        /// </summary>
        /// <param name="dmg">Base DMG value. Values should be between 0.5 and 50.0</param>
        /// <param name="attackAttribute">Attack attribute of the attacker. Might/Perception for melee/ranged or Spirit for ether</param>
        /// <param name="defense">The total defense value of the target</param>
        /// <param name="defenseAttribute">The defense attribute of the target. Vitality for melee/ranged or Spirit for ether</param>
        /// <param name="isCritical">true if critical attack, false otherwise</param>
        /// <returns>A damage value to apply to the target.</returns>
        public static int CalculateDamage(
            float dmg,
            float attackAttribute,
            float defense,
            float defenseAttribute,
            bool isCritical)
        {
            // Formula: ((DMG+attackAttribute/3)*6)-((defense+defenseAttribute/3)/3)
            var maxDamage = (dmg + attackAttribute / 3f) * 6f - ((defense + defenseAttribute / 3f) / 3f);
            var minDamage = maxDamage * 0.75f;

            // Criticals - 25% bonus to damage range
            if (isCritical)
            {
                minDamage = maxDamage;
                maxDamage *= 1.25f;
            }

            return (int)Random.NextFloat(minDamage, maxDamage);
        }
    }
}
