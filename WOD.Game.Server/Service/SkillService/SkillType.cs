using System;
using System.Collections.Generic;
using WOD.Game.Server.Enumeration;

namespace WOD.Game.Server.Service.SkillService
{
    public enum SkillType
    {
        [Skill(SkillCategoryType.Invalid,
            "Invalid",
            0,
            false,
            "Unused in-game.",
            false,
            false)]
        Invalid = 0,

        [Skill(SkillCategoryType.Combat,
            "One-Handed",
            50,
            true,
            "Ability to use one-handed weapons like swords and knives.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        OneHanded = 1,

        [Skill(SkillCategoryType.Combat,
            "Two-Handed",
            50,
            true,
            "Ability to use heavy weapons such as greatswords, polearms, and staves.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        TwoHanded = 2,

        [Skill(SkillCategoryType.Combat,
            "Brawling", 50,
            true,
            "Ability to fight hand-to-hand, use brass knuckles, or claws.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        MartialArts = 3,

        [Skill(SkillCategoryType.Combat,
            "Ranged",
            50,
            true,
            "Ability to use ranged weapons like pistols, throwing weapons, and rifles in combat.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        Ranged = 4,

        [Skill(SkillCategoryType.Combat,
            "Armor",
            50,
            true,
            "Ability to effectively wear and defend against attacks with armor.",
            true,
            false)]
        Armor = 5,

        [Skill(SkillCategoryType.Crafting,
            "Smithery",
            50,
            true,
            "Ability to create melee weapons and armor.",
            true,
            true)]
        Smithery = 6,

        [Skill(SkillCategoryType.Crafting,
            "Gunsmithing",
            50,
            true,
            "Ability to create guns.",
            true,
            true)]
        Gunsmithing = 7,

        [Skill(SkillCategoryType.Languages,
            "Hindi",
            20,
            true,
            "Ability to speak the Hindi language.",
            false,
            false)]
        Hindi = 8,

        [Skill(SkillCategoryType.Languages,
            "Mandarin",
            20,
            true,
            "Ability to speak the Mandarin language.",
            false,
            false)]
        Mandarin = 9,

        [Skill(SkillCategoryType.Languages,
            "Spanish",
            20,
            true,
            "Ability to speak the Spanish language.",
            false,
            false)]
        Spanish = 10,


        [Skill(SkillCategoryType.Languages,
            "French",
            20,
            true,
            "Ability to speak the French language.",
            false,
            false)]
        French = 11,

        [Skill(SkillCategoryType.Languages,
            "Arabic",
            20,
            true,
            "Ability to speak the Arabic language.",
            false,
            false)]
        Arabic = 12,

        [Skill(SkillCategoryType.Languages,
            "Russian", 20,
            true,
            "Ability to speak the Russian language.",
            false,
            false)]
        Russian = 13,

        [Skill(SkillCategoryType.Languages,
            "Portuguese",
            20,
            true,
            "Ability to speak the Portuguese language.",
            false,
            false)]
        Portuguese = 14,

        [Skill(SkillCategoryType.Languages,
            "Swedish",
            20,
            true,
            "Ability to speak the Swedish language.",
            false,
            false)]
        Swedish = 15,

        [Skill(SkillCategoryType.Languages,
            "Dutch",
            20,
            true,
            "Ability to speak the Dutch language.",
            false,
            false)]
        Dutch = 16,

        [Skill(SkillCategoryType.Languages,
            "German",
            20,
            true,
            "Ability to speak the German language.",
            false,
            false)]
        German = 17,

        [Skill(SkillCategoryType.Languages,
            "Greek",
            20,
            true,
            "Ability to speak the Greek language.",
            false,
            false)]
        Greek = 18,

        [Skill(SkillCategoryType.Languages,
            "Finnish",
            20,
            true,
            "Ability to speak the Finnish language.",
            false,
            false)]
        Finnish = 19,

        [Skill(SkillCategoryType.Languages,
            "Italian",
            20,
            true,
            "Ability to speak the Italian language.",
            false,
            false)]
        Italian = 20,

        [Skill(SkillCategoryType.Languages,
            "Romanian",
            20,
            true,
            "Ability to speak the Romanian language.",
            false,
            false)]
        Romanian = 21,

        [Skill(SkillCategoryType.Languages,
            "Polish",
            20,
            true,
            "Ability to speak the Polish language.",
            false,
            false)]
        Polish = 22,

        [Skill(SkillCategoryType.Languages,
            "Danish",
            20,
            true,
            "Ability to speak the Danish language.",
            false,
            false)]
        Danish = 23,

        [Skill(SkillCategoryType.Languages,
            "Czech",
            20,
            true,
            "Ability to speak the Czech language.",
            false,
            false)]
        Czech = 24,

        [Skill(SkillCategoryType.Combat,
            "Explosives",
            50,
            true,
            "Ability to use grenades, bombs, and other ordinance.",
            true,
            false,
            CombatPointCategoryType.Utility)]
        Devices = 25,

        [Skill(SkillCategoryType.Discipline,
            "Celerity",
            50,
            true,
            "Celerity is a Discipline that grants vampires supernatural quickness and reflexes.",
            true,
            false,
            CombatPointCategoryType.Utility,
            CharacterType.Kindred,
            new CharacterSubType[] { CharacterSubType.Brujah, CharacterSubType.Toreador })]
        Celerity = 26,

        [Skill(SkillCategoryType.Discipline,
            "Potence",
            50,
            true,
            "Potence is the Discipline that endows vampires with physical vigor and preternatural strength. Vampires with the Potence Discipline possess physical prowess beyond mortal bounds.",
            true,
            false,
            CombatPointCategoryType.Utility,
            CharacterType.Kindred,
            new CharacterSubType[] { CharacterSubType.Brujah, CharacterSubType.Nosferatu })]
        Potence = 27,

        [Skill(SkillCategoryType.Discipline,
            "Presence",
            50,
            true,
            "Presence is the Discipline of supernatural allure and emotional manipulation which allows Kindred to attract, sway, and control crowds.",
            true,
            false,
            CombatPointCategoryType.Utility,
            CharacterType.Kindred,
            new CharacterSubType[] { CharacterSubType.Brujah, CharacterSubType.Toreador, CharacterSubType.Ventrue })]
        Presence = 28,

        [Skill(SkillCategoryType.Discipline,
            "Animalism",
            50,
            true,
            "Animalism is a Discipline that brings the vampire closer to their animalistic nature. This not only allows them to communicate with and gain dominance over creatures of nature, but gives them influence over the Beast itself.",
            true,
            false,
            CombatPointCategoryType.Utility,
            CharacterType.Kindred,
            new CharacterSubType[] { CharacterSubType.Gangrel, CharacterSubType.Nosferatu})]
        Animalism = 29,

        [Skill(SkillCategoryType.Discipline,
            "Fortitude",
            50,
            true,
            "Fortitude is a Discipline that grants Kindred unearthly toughness, even to the point of resisting fire and sunlight.",
            true,
            false,
            CombatPointCategoryType.Utility,
            CharacterType.Kindred,
            new CharacterSubType[] { CharacterSubType.Gangrel, CharacterSubType.Ventrue })]
        Fortitude = 30,

        [Skill(SkillCategoryType.Discipline,
            "Protean",
            50,
            true,
            "Protean is a Discipline that gives vampires the ability to change form, from growing feral claws to evaporating into a cloud of mist.",
            true,
            false,
            CombatPointCategoryType.Utility,
            CharacterType.Kindred,
            new CharacterSubType[] { CharacterSubType.Gangrel })]
        Protean = 31,

        [Skill(SkillCategoryType.Discipline,
            "Obfuscate",
            50,
            true,
            "Obfuscate is a Discipline that allows vampires to conceal themselves, deceive the mind of others, or make them ignore what the user does not want to be seen.",
            true,
            false,
            CombatPointCategoryType.Utility,
            CharacterType.Kindred,
            new CharacterSubType[] { CharacterSubType.Nosferatu, CharacterSubType.Malkavian })]
        Obfuscate = 32,

        [Skill(SkillCategoryType.Discipline,
            "Dominate",
            50,
            true,
            "Dominate is a Discipline that overwhelms another person's mind with the vampire's will, forcing victims to think or act according to the vampire's decree.",
            true,
            false,
            CombatPointCategoryType.Utility,
            CharacterType.Kindred,
            new CharacterSubType[] { CharacterSubType.Tremere, CharacterSubType.Ventrue })]
        Dominate = 33,

        [Skill(SkillCategoryType.Discipline,
            "Auspex",
            50,
            true,
            "Auspex is a Discipline that grants vampires supernatural senses.",
            true,
            false,
            CombatPointCategoryType.Utility,
            CharacterType.Kindred,
            new CharacterSubType[] { CharacterSubType.Tremere, CharacterSubType.Malkavian, CharacterSubType.Toreador })]
        Auspex = 34,


        [Skill(SkillCategoryType.Discipline,
            "Dementation",
            50,
            true,
            "Dementation is a Discipline that draws on the vampire's own insanity and uses it to achieve profound insights or inflict madness upon others.",
            true,
            false,
            CombatPointCategoryType.Utility,
            CharacterType.Kindred,
            new CharacterSubType[] { CharacterSubType.Malkavian})]
        Dementation = 35,

        [Skill(SkillCategoryType.Discipline,
            "Thaumaturgy",
            50,
            true,
            "Thaumaturgy is the closely guarded form of blood magic practiced by the vampiric clan Tremere.",
            true,
            false,
            CombatPointCategoryType.Utility,
            CharacterType.Kindred,
            new CharacterSubType[] { CharacterSubType.Tremere })]
        Thaumaturgy = 36,
    }

    public class SkillAttribute : Attribute
    {
        public SkillCategoryType Category { get; set; }
        public string Name { get; set; }
        public int MaxRank { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public bool ContributesToSkillCap { get; set; }
        public bool IsShownInCraftMenu { get; set; }
        public CharacterType CharacterTypeRestriction { get; set; }
        public CombatPointCategoryType CombatPointCategory { get; set; }
        public CharacterSubType[] CharacterSubTypeRestrictions { get; set; }

        public SkillAttribute(
                SkillCategoryType category,
                string name,
                int maxRank,
                bool isActive,
                string description,
                bool contributesToSkillCap,
                bool isShownInCraftMenu,
                CombatPointCategoryType combatPointCategory = CombatPointCategoryType.Exempt,
                CharacterType characterTypeRestriction = CharacterType.Invalid,
                params CharacterSubType[] characterSubTypeRestrictions)
        {
            Category = category;
            Name = name;
            MaxRank = maxRank;
            IsActive = isActive;
            Description = description;
            ContributesToSkillCap = contributesToSkillCap;
            IsShownInCraftMenu = isShownInCraftMenu;
            CharacterTypeRestriction = characterTypeRestriction;
            CombatPointCategory = combatPointCategory;
            CharacterSubTypeRestrictions = characterSubTypeRestrictions;
        }
    }
}
