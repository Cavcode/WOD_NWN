using System.Collections.Generic;
using WOD.Game.Server.Core.NWNX;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.SkillService;
using static WOD.Game.Server.Core.NWScript.NWScript;
using Item = WOD.Game.Server.Service.Item;

namespace WOD.Game.Server.Feature.PerkDefinition
{
    public class DisciplinePerkDefinition : IPerkListDefinition
    {
        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            var builder = new PerkBuilder();
            Awe(builder);

            return builder.Build();
        }

        private void Awe(PerkBuilder builder)
        {
            builder.Create(PerkCategoryType.Presence, PerkType.Awe)
                .Name("Awe")

                .AddPerkLevel()
                .Description("Those very close to the player suffer a -2 penalty to Might, Willpower, and Dexterity, and a reduced rate of attack. Allies benefit from the opposite of this effect. Lasts 16 seconds per activation. May add extra dialogue options with NPCs.")
                .Price(3)
                .GrantsFeat(FeatType.Awe);
        }
    }
}
