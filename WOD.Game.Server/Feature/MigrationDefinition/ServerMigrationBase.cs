using WOD.Game.Server.Entity;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.DBService;

namespace WOD.Game.Server.Feature.MigrationDefinition
{
    public abstract class ServerMigrationBase
    {
        protected void GrantRebuildTokenToAllPlayers()
        {
            var query = new DBQuery<Player>();
            var count = (int)DB.SearchCount(query);
            var dbPlayers = DB.Search(query
                .AddPaging(count, 0));

            foreach (var dbPlayer in dbPlayers)
            {
                dbPlayer.NumberRebuildsAvailable++;
                DB.Set(dbPlayer);
            }
        }
    }
}
