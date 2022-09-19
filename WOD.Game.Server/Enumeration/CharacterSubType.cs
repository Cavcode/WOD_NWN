using System;

namespace WOD.Game.Server.Enumeration
{
    public enum CharacterSubType
    {
        [CharacterSubType("Invalid")]
        Invalid = 0,
        [CharacterSubType("Brujah")]
        Brujah = 1,
        [CharacterSubType("Gangrel")]
        Gangrel = 2,
        [CharacterSubType("Malkavian")]
        Malkavian = 3,
        [CharacterSubType("Nosferatu")]
        Nosferatu = 4,
        [CharacterSubType("Toreador")]
        Toreador = 5,
        [CharacterSubType("Tremere")]
        Tremere = 6,
        [CharacterSubType("Ventrue")]
        Ventrue = 7,
    }

    public class CharacterSubTypeAttribute : Attribute
    {
        public string Name { get; set; }

        public CharacterSubTypeAttribute(string name)
        {
            Name = name;
        }
    }
}
