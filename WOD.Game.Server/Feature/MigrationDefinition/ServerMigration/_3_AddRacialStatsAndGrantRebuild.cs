using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.DBService;
using WOD.Game.Server.Service.MigrationService;

namespace WOD.Game.Server.Feature.MigrationDefinition.ServerMigration
{
    public class _3_AddRacialStatsAndGrantRebuild: ServerMigrationBase
    {
        public int Version => 3;
        public void Migrate()
        {
            var query = new DBQuery<Player>();
            var playerCount = (int)DB.SearchCount(query);
            var players = DB.Search(query.AddPaging(playerCount, 0));

            foreach (var player in players)
            {
                player.RacialStat = AbilityType.Invalid;
                player.NumberRebuildsAvailable = 1;

                DB.Set(player);
            }
        }
    }
}
