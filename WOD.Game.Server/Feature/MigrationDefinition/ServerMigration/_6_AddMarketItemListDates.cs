using System;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.DBService;
using WOD.Game.Server.Service.MigrationService;

namespace WOD.Game.Server.Feature.MigrationDefinition.ServerMigration
{
    public class _6_AddMarketItemListDates : IServerMigration
    {
        public int Version => 6;
        public void Migrate()
        {
            var query = new DBQuery<MarketItem>()
                .AddFieldSearch(nameof(MarketItem.IsListed), true);
            var count = (int)DB.SearchCount(query);
            var listings = DB.Search(query
                .AddPaging(count, 0));
            var now = DateTime.UtcNow;


            foreach (var listing in listings)
            {
                listing.DateListed = now;
                DB.Set(listing);
            }
        }
    }
}
