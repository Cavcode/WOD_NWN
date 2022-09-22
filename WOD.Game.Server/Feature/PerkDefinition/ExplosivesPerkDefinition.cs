using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.SkillService;

namespace WOD.Game.Server.Feature.PerkDefinition
{
    public class ExplosivesPerkDefinition: IPerkListDefinition
    {
        private readonly PerkBuilder _builder = new();

        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            DemolitionExpert();
            FragGrenade();
            ConcussionGrenade();
            FlashbangGrenade();
            AdhesiveGrenade();
            SmokeBomb();
            IncendiaryBomb();
            GasBomb();

            return _builder.Build();
        }

        private void DemolitionExpert()
        {
            _builder.Create(PerkCategoryType.Explosives, PerkType.DemolitionExpert)
                .Name("Demolition Expert")

                .AddPerkLevel()
                .Description("10% chance to use a Devices ability without consuming explosives.")
                .Price(1)
                .RequirementSkill(SkillType.Devices, 10)
                .GrantsFeat(FeatType.DemolitionExpert1)

                .AddPerkLevel()
                .Description("20% chance to use a Devices ability without consuming explosives.")
                .Price(2)
                .RequirementSkill(SkillType.Devices, 25)
                .GrantsFeat(FeatType.DemolitionExpert2)

                .AddPerkLevel()
                .Description("30% chance to use a Devices ability without consuming explosives.")
                .Price(3)
                .RequirementSkill(SkillType.Devices, 40)
                .GrantsFeat(FeatType.DemolitionExpert3);
        }

        private void FragGrenade()
        {
            _builder.Create(PerkCategoryType.Explosives, PerkType.FragGrenade)
                .Name("Frag Grenade")

                .AddPerkLevel()
                .Description("Deals 6 fire DMG to all creatures within range of explosion. Consumes explosives on use.")
                .Price(2)
                .GrantsFeat(FeatType.FragGrenade1)

                .AddPerkLevel()
                .Description("Deals 10 fire DMG to all creatures within range of explosion. Also has a 30% chance to inflict Bleeding. Consumes explosives on use.")
                .Price(3)
                .RequirementSkill(SkillType.Devices, 15)
                .GrantsFeat(FeatType.FragGrenade2)

                .AddPerkLevel()
                .Description("Deals 16 fire DMG to all creatures within range of explosion. Also has a 50% chance to inflict Bleeding. Consumes explosives on use.")
                .Price(3)
                .RequirementSkill(SkillType.Devices, 35)
                .GrantsFeat(FeatType.FragGrenade3);
        }

        private void ConcussionGrenade()
        {
            _builder.Create(PerkCategoryType.Explosives, PerkType.ConcussionGrenade)
                .Name("Concussion Grenade")

                .AddPerkLevel()
                .Description("Deals 6 electrical DMG to all creatures within range of explosion. Consumes explosives on use.")
                .RequirementSkill(SkillType.Devices, 5)

                .Price(2)
                .GrantsFeat(FeatType.ConcussionGrenade1)

                .AddPerkLevel()
                 .Description("Deals 10 electrical DMG to all creatures within range of explosion. Also has a 30% chance to inflict Knockdown. Consumes explosives on use.")
                .Price(3)
                .RequirementSkill(SkillType.Devices, 30)

                .GrantsFeat(FeatType.ConcussionGrenade2)

                .AddPerkLevel()
                .Description("Deals 16 electrical DMG to all creatures within range of explosion. Also has a 50% chance to inflict Knockdown. Consumes explosives on use.")
                .Price(3)
                .RequirementSkill(SkillType.Devices, 45)

                .GrantsFeat(FeatType.ConcussionGrenade3);
        }

        private void FlashbangGrenade()
        {
            _builder.Create(PerkCategoryType.Explosives, PerkType.FlashbangGrenade)
                .Name("Flashbang Grenade")

                .AddPerkLevel()
                .Description("Reduces Attack Bonus by 2 on all enemies within range of explosion for 20 seconds. Consumes explosives on use.")
                .RequirementSkill(SkillType.Devices, 10)

                .Price(2)
                .GrantsFeat(FeatType.FlashbangGrenade1)

                .AddPerkLevel()
                .Description("Reduces Attack Bonus by 4 on all enemies within range of explosion for 20 seconds. Consumes explosives on use.")
                .Price(3)
                .RequirementSkill(SkillType.Devices, 30)

                .GrantsFeat(FeatType.FlashbangGrenade2)

                .AddPerkLevel()
                .Description("Reduces Attack Bonus by 6 on all enemies within range of explosion for 20 seconds. Consumes explosives on use.")
                .Price(3)
                .RequirementSkill(SkillType.Devices, 45)

                .GrantsFeat(FeatType.FlashbangGrenade3);
        }

        private void AdhesiveGrenade()
        {
            _builder.Create(PerkCategoryType.Explosives, PerkType.AdhesiveGrenade)
                .Name("Adhesive Grenade")

                .AddPerkLevel()
                .Description("Inflicts slow on all enemies within range of explosion for 4 seconds. Consumes explosives on use.")
                .RequirementSkill(SkillType.Devices, 25)

                .Price(2)
                .GrantsFeat(FeatType.AdhesiveGrenade1)

                .AddPerkLevel()
                .Description("Inflicts slow on all enemies within range of explosion for 6 seconds. Also has a 30% chance to inflict immobilize instead. Consumes explosives on use.")
                .Price(3)
                .RequirementSkill(SkillType.Devices, 40)

                .GrantsFeat(FeatType.AdhesiveGrenade2)

                .AddPerkLevel()
                .Description("Inflicts slow on all enemies within range of explosion for 8 seconds. Also has a 50% chance to inflict immobilize instead. Consumes explosives on use.")
                .Price(3)
                .RequirementSkill(SkillType.Devices, 50)

                .GrantsFeat(FeatType.AdhesiveGrenade3);
        }

        private void SmokeBomb()
        {
            _builder.Create(PerkCategoryType.Explosives, PerkType.SmokeBomb)
                .Name("Smoke Bomb")

                .AddPerkLevel()
                .Description("Creates a smokescreen at the explosion site, granting invisibility to all creatures who enter the area of effect for 20 seconds. Consumes explosives on use.")
                .RequirementSkill(SkillType.Devices, 8)

                .Price(2)
                .GrantsFeat(FeatType.SmokeBomb1)

                .AddPerkLevel()
                .Description("Creates a smokescreen at the explosion site, granting invisibility to all creatures who enter the area of effect for 40 seconds. Consumes explosives on use.")
                .Price(3)
                .RequirementSkill(SkillType.Devices, 28)

                .GrantsFeat(FeatType.SmokeBomb2)

                .AddPerkLevel()
                .Description("Creates a smokescreen at the explosion site, granting invisibility to all creatures who enter the area of effect for 60 seconds. Consumes explosives on use.")
                .Price(3)
                .RequirementSkill(SkillType.Devices, 48)

                .GrantsFeat(FeatType.SmokeBomb3);
        }

        private void IncendiaryBomb()
        {
            _builder.Create(PerkCategoryType.Explosives, PerkType.IncendiaryBomb)
                .Name("Incendiary Bomb")

                .AddPerkLevel()
                .Description("Creates a fire field at the explosion site, dealing 4 fire DMG to all creatures who enter the area of effect for 20 seconds. Consumes explosives on use.")
                .RequirementSkill(SkillType.Devices, 13)

                .Price(2)
                .GrantsFeat(FeatType.IncendiaryBomb1)

                .AddPerkLevel()
                .Description("Creates a fire field at the explosion site, dealing 10 fire DMG to all creatures who enter the area of effect for 40 seconds. Consumes explosives on use.")
                .Price(3)
                .RequirementSkill(SkillType.Devices, 33)

                .GrantsFeat(FeatType.IncendiaryBomb2)

                .AddPerkLevel()
                .Description("Creates a fire field at the explosion site, dealing 16 fire DMG to all creatures who enter the area of effect for 60 seconds. Consumes explosives on use.")
                .Price(3)
                .RequirementSkill(SkillType.Devices, 43)

                .GrantsFeat(FeatType.IncendiaryBomb3);
        }

        private void GasBomb()
        {
            _builder.Create(PerkCategoryType.Explosives, PerkType.GasBomb)
                .Name("Gas Bomb")

                .AddPerkLevel()
                .Description("Creates a poison field at the explosion site, dealing 4 poison DMG to all creatures who enter the area of effect for 18 seconds. Consumes explosives on use.")
                .RequirementSkill(SkillType.Devices, 16)

                .Price(2)
                .GrantsFeat(FeatType.GasBomb1)

                .AddPerkLevel()
                .Description("Creates a poison field at the explosion site, dealing 12 poison DMG to all creatures who enter the area of effect for 30 seconds. Consumes explosives on use.")
                .Price(3)
                .RequirementSkill(SkillType.Devices, 34)

                .GrantsFeat(FeatType.GasBomb2)

                .AddPerkLevel()
                .Description("Creates a poison field at the explosion site, dealing 16 poison DMG to all creatures who enter the area of effect for 48 seconds. Consumes explosives on use.")
                .Price(3)
                .RequirementSkill(SkillType.Devices, 46)

                .GrantsFeat(FeatType.GasBomb3);
        }
    }
}
