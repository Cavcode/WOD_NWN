namespace WOD.Game.Server.Entity
{
    public class ServerConfiguration: EntityBase
    {
        public ServerConfiguration()
        {
            Id = "WOD_CONFIG";
            MigrationVersion = 0;
        }

        [Indexed]
        public int MigrationVersion { get; set; }
    }
}
