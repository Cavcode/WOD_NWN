using System;

namespace WOD.Game.Server.Service.SkillService
{
    public enum SkillType
    {
        [Skill(SkillCategoryType.Invalid, 
            "Invalid", 
            0, 
            false, 
            "Unused in-game.", 
            false)]
        Invalid = 0,

        // Melee Combat
        [Skill(SkillCategoryType.Combat, 
            "One-Handed", 
            50, 
            true, 
            "Ability to use one-handed weapons like vibroblades, finesse vibroblades, and lightsabers.", 
            true)]
        OneHanded = 1,

        [Skill(SkillCategoryType.Combat, 
            "Two-Handed", 
            50, 
            true, 
            "Ability to use heavy weapons like heavy vibroblades, polearms, and saberstaffs in combat.", 
            true)]
        TwoHanded = 2,

        [Skill(SkillCategoryType.Combat, 
            "Martial Arts", 50, 
            true, 
            "Ability to fight using katars and staves in combat.", 
            true)]
        MartialArts = 3,

        [Skill(SkillCategoryType.Combat, 
            "Ranged", 
            50, 
            true, 
            "Ability to use ranged weapons like pistols, cannons, and rifles in combat.", 
            true)]
        Ranged = 4,
        [Skill(SkillCategoryType.Combat, 
            "Armor", 
            50, 
            true,
            "Ability to effectively wear and defend against attacks with armor.", 
            true)]
        Armor = 6,

        // Utility
        [Skill(SkillCategoryType.Utility,
            "Piloting",
            50,
            true,
            "Ability to pilot starships, follow navigation charts, and control starship systems.",
            true)]
        Piloting = 7,

        [Skill(SkillCategoryType.Utility,
            "First Aid",
            50,
            true,
            "Ability to treat bodily injuries in the field with healing kits and stim packs.",
            true)]
        FirstAid = 8,

        // Crafting
        [Skill(SkillCategoryType.Crafting, 
            "Smithery", 
            50, 
            true, 
            "Ability to create weapons and armor like vibroblades, blasters, and helmets.", 
            true)]
        Smithery = 9,
        
        [Skill(SkillCategoryType.Crafting, 
            "Fabrication", 
            50, 
            true, 
            "Ability to create base structures, furniture, and starships.", 
            true)]
        Fabrication = 10,

        [Skill(SkillCategoryType.Crafting, 
            "Gathering", 
            50, 
            true, 
            "Ability to harvest raw materials and scavenge for supplies.", 
            true)]
        Gathering = 11,

        [Skill(SkillCategoryType.Utility,
            "Diplomacy",
            20,
            true,
            "Ability to handle people, negotiate, and manage relations.",
            true)]
        Diplomacy = 12,
        // Disciplines
        [Skill(SkillCategoryType.Disciplines,
            "Animalism",
            50,
            true,
            "Animalism is a Discipline that brings the vampire closer to their animalistic nature.",
            true)]
        Animalism = 13,
        [Skill(SkillCategoryType.Disciplines,
            "Auspex",
            50,
            true,
            "Auspex is a Discipline that grants Kindred supernatural senses.",
            true)]
        Auspex = 14,
        [Skill(SkillCategoryType.Disciplines,
            "Celerity",
            50,
            true,
            "Celerity is a powerful Discipline which allows Kindred to move at an incredible speed.",
            true)]
        Celerity = 15,
        [Skill(SkillCategoryType.Disciplines,
            "Dementation",
            50,
            true,
            "Dementation gives the Kindred the ability to control other people's minds, to induce hypnotic suggestions, or provoke suicides.",
            true)]
        Dementation = 16,
        [Skill(SkillCategoryType.Disciplines,
            "Dominate",
            50,
            true,
            "Dominate is a Discipline that overwhelms another person's mind with the vampire's will, forcing victims to think or act according to the vampire's decree. ",
            true)]
        Dominate = 17,
        [Skill(SkillCategoryType.Disciplines,
            "Fortitude",
            50,
            true,
            "Fortitude grants Kindred incredible resilience and the ability to resist fire and sunglight.",
            true)]
        Fortitude = 18,
        [Skill(SkillCategoryType.Disciplines,
            "Obfuscate",
            50,
            true,
            "Obfuscate is a Discipline that allows vampires to conceal themselves and create some manner of illusions.",
            true)]
        Obfuscate = 19,
        [Skill(SkillCategoryType.Disciplines,
            "Potence",
            50,
            true,
            "Potence grants Kindred supernatural strength.",
            true)]
        Potence = 20,
        [Skill(SkillCategoryType.Disciplines,
            "Presence",
            50,
            true,
            "Many Presence powers can be used upon large groups of people at once and transcend virtually all boundaries of gender, race, religion, class, and supernatural status. ",
            true)]
        Presence = 21,
        [Skill(SkillCategoryType.Disciplines,
            "Protean",
            50,
            true,
            "Protean is the signature Discipline of clan Gangrel. It uses their close relationship with nature and the Beast within themselves.",
            true)]
        Protean = 22,
        [Skill(SkillCategoryType.Disciplines,
            "Thaumaturgy",
            50,
            true,
            "Thaumaturgy is immensely powerful and has made the Tremere feared and hated by their fellow Kindred.  It revolves around Blood Magic; it manipulates blood in such a way that a Tremere may steal it from others, use it to craft protective armor, or heat it enough to make a person explode.",
            true)]
        Thaumaturgy = 23,
        // Languages
        [Skill(SkillCategoryType.Languages,
            "English",
            50,
            true,
            "The English language.",
            false)]
        English = 24,
        [Skill(SkillCategoryType.Languages,
            "Mandarin",
            50,
            true,
            "The Mandarin language.",
            false)]
        Mandarin = 25,
        [Skill(SkillCategoryType.Languages,
            "Spanish",
            50,
            true,
            "The Spanish language.",
            false)]
        Spanish = 26,
        [Skill(SkillCategoryType.Languages,
            "French",
            50,
            true,
            "The French language.",
            false)]
        French = 27,
        [Skill(SkillCategoryType.Languages,
            "Arabic",
            50,
            true,
            "The Arabic language.",
            false)]
        Arabic = 28,
        [Skill(SkillCategoryType.Languages,
            "Russian",
            50,
            true,
            "The Rusisan language.",
            false)]
        Russian = 29,
        [Skill(SkillCategoryType.Languages,
            "Portuguese ",
            50,
            true,
            "The Portuguese  language.",
            false)]
        Portuguese = 30,
        [Skill(SkillCategoryType.Languages,
            "German",
            50,
            true,
            "The German  language.",
            false)]
        German = 31,
        [Skill(SkillCategoryType.Languages,
            "Dutch",
            50,
            true,
            "The German  language.",
            false)]
        Dutch = 32,
        [Skill(SkillCategoryType.Languages,
            "Hindi",
            50,
            true,
            "The Hindi  language.",
            false)]
        Hindi = 33,
    }

    public class SkillAttribute : Attribute
    {
        public SkillCategoryType Category { get; set; }
        public string Name { get; set; }
        public int MaxRank { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public bool ContributesToSkillCap { get; set; }

        public SkillAttribute(
            SkillCategoryType category,
            string name,
            int maxRank,
            bool isActive,
            string description,
            bool contributesToSkillCap)
        {
            Category = category;
            Name = name;
            MaxRank = maxRank;
            IsActive = isActive;
            Description = description;
            ContributesToSkillCap = contributesToSkillCap;
        }
    }
}
