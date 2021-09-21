﻿using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWNX;
using WOD.Game.Server.Core.NWNX.Enum;
using WOD.Game.Server.Core.NWScript.Enum;

namespace WOD.Game.Server.Feature
{
    public static class FeatConfiguration
    {
        /// <summary>
        /// When the module loads, configure all custom feats.
        /// </summary>
        [NWNEventHandler("mod_load")]
        public static void ConfigureFeats()
        {
            FeatPlugin.SetFeatModifier(FeatType.ShieldConcealment1, FeatModifierType.Concealment, 5);
            FeatPlugin.SetFeatModifier(FeatType.ShieldConcealment2, FeatModifierType.Concealment, 10);
            FeatPlugin.SetFeatModifier(FeatType.ShieldConcealment3, FeatModifierType.Concealment, 15);
            FeatPlugin.SetFeatModifier(FeatType.ShieldConcealment4, FeatModifierType.Concealment, 20);
            FeatPlugin.SetFeatModifier(FeatType.ShieldConcealment5, FeatModifierType.Concealment, 25);
        }
    }
}
