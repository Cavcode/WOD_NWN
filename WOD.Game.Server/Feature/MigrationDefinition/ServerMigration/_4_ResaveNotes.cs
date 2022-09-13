using WOD.Game.Server.Entity;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.DBService;
using WOD.Game.Server.Service.MigrationService;

namespace WOD.Game.Server.Feature.MigrationDefinition.ServerMigration
{
    public class _4_ResaveNotes : ServerMigrationBase
    {
        public int Version => 4;
        public void Migrate()
        {
            var query = new DBQuery<PlayerNote>();
            var noteCount = (int)DB.SearchCount(query);
            var notes = DB.Search(query.AddPaging(noteCount, 0));

            foreach (var note in notes)
            {
                note.DMCreatorCDKey = string.Empty;
                note.DMCreatorName = string.Empty;
                note.IsDMNote = false;

                DB.Set(note);
            }
        }
    }
}
