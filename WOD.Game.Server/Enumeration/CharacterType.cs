using System;

namespace WOD.Game.Server.Enumeration
{
    public enum CharacterType
    {
        [CharacterType("Invalid")]
        Invalid = 0,
        [CharacterType("Kine")]
        Kine = 1,
        [CharacterType("Kindred")]
        Kindred = 2,
    }

    public class CharacterTypeAttribute : Attribute
    {
        public string Name { get; set; }

        public CharacterTypeAttribute(string name)
        {
            Name = name;
        }
    }
}
