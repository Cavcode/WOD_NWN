using WOD.Game.Server.Enumeration;

namespace WOD.Game.Server.Entity
{
    public class AuthorizedDM: EntityBase
    {
        public string Name { get; set; }
        public string CDKey { get; set; }
        public AuthorizationLevel Authorization { get; set; }
        public override string KeyPrefix => "AuthorizedDM";
    }
}
