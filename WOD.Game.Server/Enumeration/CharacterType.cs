using System;

namespace WOD.Game.Server.Enumeration
{
    public enum CharacterType
    {
        Invalid = 0,
        Brujah = 1,
        Tremere = 2,
        Gangrel = 3,
        Malkavian = 4,
        Nosferatu = 5,
        Toreador = 6,
        Ventrue = 7,
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
