using System;

namespace WOD.Game.Server.Enumeration
{
    public enum GuildType
    {
        [Guild("Unknown", false)]
        Invalid = 0,
        [Guild("Hunter's Guild", true)]
        HuntersGuild = 1,
        [Guild("Gunsmithing Guild", true)]
        GunsmithingGuild = 2,
        [Guild("Smithery Guild", true)]
        SmitheryGuild = 3,
        [Guild("Construction Guild", true)]
        ConstructionGuild = 4,
        [Guild("Agriculture Guild", true)]
        AgricultureGuild = 5,
    }

    public class GuildAttribute : Attribute
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public GuildAttribute(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }
    }
}
