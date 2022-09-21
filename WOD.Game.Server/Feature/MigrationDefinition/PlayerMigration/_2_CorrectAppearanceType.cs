using System.Linq;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.Creature;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Feature.AppearanceDefinition.RacialAppearance;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.MigrationService;

namespace WOD.Game.Server.Feature.MigrationDefinition.PlayerMigration
{
    public class _2_CorrectAppearanceType: IPlayerMigration
    {
        public int Version => 2;
        public void Migrate(uint player)
        {
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);
            var racialType = GetRacialType(player);
            AppearanceType appearanceType;
            int headId;
            var gender = GetGender(player);

            switch (racialType)
            {
                case RacialType.Human:
                    appearanceType = AppearanceType.Human;
                    if (gender == Gender.Male)
                        headId = new HumanRacialAppearanceDefinition().MaleHeads.First();
                    else
                        headId = new HumanRacialAppearanceDefinition().FemaleHeads.First();
                    break;
                default:
                    appearanceType = AppearanceType.Human;
                    if (gender == Gender.Male)
                        headId = new HumanRacialAppearanceDefinition().MaleHeads.First();
                    else
                        headId = new HumanRacialAppearanceDefinition().FemaleHeads.First();
                    break;
            }

            SetCreatureAppearanceType(player, appearanceType);
            SetCreatureBodyPart(CreaturePart.Head, headId, player);

            dbPlayer.OriginalAppearanceType = GetAppearanceType(player);
            DB.Set(dbPlayer);
        }
    }
}
