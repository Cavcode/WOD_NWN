using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service.PerkService;
using WOD.Game.Server.Service.SkillService;

namespace WOD.Game.Server.Feature.PerkDefinition
{
    public class FirstAidPerkDefinition : IPerkListDefinition
    {
        private readonly PerkBuilder _builder = new PerkBuilder();

        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            RangedHealing();
            FrugalMedic();
            MedKit();
            BactaRecovery();
            Resuscitation();
            TreatmentKit();
            StasisField();
            CombatEnhancement();
            Shielding();

            return _builder.Build();
        }

        private void RangedHealing()
        {
            _builder.Create(PerkCategoryType.FirstAid, PerkType.RangedHealing)
                .Name("Ranged Healing")

                .AddPerkLevel()
                .Description("Increases the range you can use First Aid abilities by 1 meter.")
                .Price(2)
                .RequirementSkill(SkillType.FirstAid, 5)
                .GrantsFeat(FeatType.RangedHealing1)

                .AddPerkLevel()
                .Description("Increases the range you can use First Aid abilities by 2 meters.")
                .Price(3)
                .RequirementSkill(SkillType.FirstAid, 15)
                .GrantsFeat(FeatType.RangedHealing2)

                .AddPerkLevel()
                .Description("Increases the range you can use First Aid abilities by 3 meters.")
                .Price(4)
                .RequirementSkill(SkillType.FirstAid, 25)
                
                .GrantsFeat(FeatType.RangedHealing3)

                .AddPerkLevel()
                .Description("Increases the range you can use First Aid abilities by 4 meters.")
                .Price(5)
                .RequirementSkill(SkillType.FirstAid, 35)
                
                .GrantsFeat(FeatType.RangedHealing4);
        }

        private void FrugalMedic()
        {
            _builder.Create(PerkCategoryType.FirstAid, PerkType.FrugalMedic)
                .Name("Frugal Medic")

                .AddPerkLevel()
                .Description("10% chance to use a First Aid ability without consuming supplies or stim packs.")
                .Price(1)
                .RequirementSkill(SkillType.FirstAid, 10)
                .GrantsFeat(FeatType.FrugalMedic1)

                .AddPerkLevel()
                .Description("20% chance to use a First Aid ability without consuming supplies or stim packs.")
                .Price(2)
                .RequirementSkill(SkillType.FirstAid, 25)
                
                .GrantsFeat(FeatType.FrugalMedic2)

                .AddPerkLevel()
                .Description("30% chance to use a First Aid ability without consuming supplies or stim packs.")
                .Price(2)
                .RequirementSkill(SkillType.FirstAid, 40)
                
                .GrantsFeat(FeatType.FrugalMedic3);
        }

        private void MedKit()
        {
            _builder.Create(PerkCategoryType.FirstAid, PerkType.MedKit)
                .Name("Med Kit")

                .AddPerkLevel()
                .Description("Restores a single target's HP. Consumes medical supplies on use.")
                .Price(1)
                .GrantsFeat(FeatType.MedKit1)

                .AddPerkLevel()
                .Description("Restores a single target's HP. Consumes medical supplies on use.")
                .Price(2)
                .RequirementSkill(SkillType.FirstAid, 10)
                .GrantsFeat(FeatType.MedKit2)

                .AddPerkLevel()
                .Description("Restores a single target's HP. Consumes medical supplies on use.")
                .Price(3)
                .RequirementSkill(SkillType.FirstAid, 20)
                .GrantsFeat(FeatType.MedKit3)

                .AddPerkLevel()
                .Description("Restores a single target's HP. Consumes medical supplies on use.")
                .Price(4)
                .RequirementSkill(SkillType.FirstAid, 30)
                .GrantsFeat(FeatType.MedKit4)

                .AddPerkLevel()
                .Description("Restores a single target's HP. Consumes medical supplies on use.")
                .Price(4)
                .RequirementSkill(SkillType.FirstAid, 40)
                .GrantsFeat(FeatType.MedKit5);
        }

        private void BactaRecovery()
        {
            _builder.Create(PerkCategoryType.FirstAid, PerkType.BactaRecovery)
                .Name("Bacta Recovery")

                .AddPerkLevel()
                .Description("Restores HP to all allies within 3 meters of you. Consumes medical supplies on use.")
                .Price(3)
                .RequirementSkill(SkillType.FirstAid, 15)
                
                .GrantsFeat(FeatType.BactaRecovery1)

                .AddPerkLevel()
                .Description("Restores HP to all allies within 3 meters of you. Consumes medical supplies on use.")
                .Price(4)
                .RequirementSkill(SkillType.FirstAid, 30)
                
                .GrantsFeat(FeatType.BactaRecovery2)

                .AddPerkLevel()
                .Description("Restores HP to all allies within 3 meters of you. Consumes medical supplies on use.")
                .Price(5)
                .RequirementSkill(SkillType.FirstAid, 45)
                
                .GrantsFeat(FeatType.BactaRecovery3);
        }

        private void Resuscitation()
        {
            _builder.Create(PerkCategoryType.FirstAid, PerkType.Resuscitation)
                .Name("Resuscitation")

                .AddPerkLevel()
                .Description("Revives a single target back from the brink of death with 1 HP. Consumes medical supplies on use.")
                .Price(4)
                .RequirementSkill(SkillType.FirstAid, 20)
                
                .GrantsFeat(FeatType.Resuscitation1)

                .AddPerkLevel()
                .Description("Revives a single target back from the brink of death with 25% HP. Consumes medical supplies on use.")
                .Price(4)
                .RequirementSkill(SkillType.FirstAid, 30)
                
                .GrantsFeat(FeatType.Resuscitation2)

                .AddPerkLevel()
                .Description("Revives a single target back from the brink of death with 50% HP. Consumes medical supplies on use.")
                .Price(4)
                .RequirementSkill(SkillType.FirstAid, 40)
                
                .GrantsFeat(FeatType.Resuscitation3);
        }

        private void TreatmentKit()
        {
            _builder.Create(PerkCategoryType.FirstAid, PerkType.TreatmentKit)
                .Name("Treatment Kit")

                .AddPerkLevel()
                .Description("Removes bleed and poison from a single target. Consumes medical supplies on use.")
                .Price(2)
                .RequirementSkill(SkillType.FirstAid, 5)
                .GrantsFeat(FeatType.TreatmentKit1)

                .AddPerkLevel()
                .Description("Removes bleed, poison, shock, and burn from a single target. Consumes medical supplies on use.")
                .Price(2)
                .RequirementSkill(SkillType.FirstAid, 15)
                
                .GrantsFeat(FeatType.TreatmentKit2);
        }

        private void StasisField()
        {
            _builder.Create(PerkCategoryType.FirstAid, PerkType.StasisField)
                .Name("Stasis Field")

                .AddPerkLevel()
                .Description("Increase your target's Evasion by 2 for 15 minutes. Consumes stim pack on use.")
                .Price(2)
                .RequirementSkill(SkillType.FirstAid, 20)
                
                .GrantsFeat(FeatType.StasisField1)

                .AddPerkLevel()
                .Description("Increase your target's Evasion by 4 for 15 minutes. Consumes stim pack on use.")
                .Price(3)
                .RequirementSkill(SkillType.FirstAid, 30)
                
                .GrantsFeat(FeatType.StasisField2)

                .AddPerkLevel()
                .Description("Increase your target's Evasion by 6 for 15 minutes. Consumes stim pack on use.")
                .Price(4)
                .RequirementSkill(SkillType.FirstAid, 40)
                
                .GrantsFeat(FeatType.StasisField3);
        }

        private void CombatEnhancement()
        {
            _builder.Create(PerkCategoryType.FirstAid, PerkType.CombatEnhancement)
                .Name("Combat Enhancement")

                .AddPerkLevel()
                .Description("Increases the MGT, PER, and VIT of a single target by 2 for 15 minutes. Consumes stim pack on use.")
                .Price(3)
                .RequirementSkill(SkillType.FirstAid, 25)
                
                .GrantsFeat(FeatType.CombatEnhancement1)

                .AddPerkLevel()
                .Description("Increases the MGT, PER, and VIT of a single target by 4 for 15 minutes. Consumes stim pack on use.")
                .Price(3)
                .RequirementSkill(SkillType.FirstAid, 35)
                
                .GrantsFeat(FeatType.CombatEnhancement2)

                .AddPerkLevel()
                .Description("Increases the MGT, PER, and VIT of a single target by 6 for 15 minutes. Consumes stim pack on use.")
                .Price(4)
                .RequirementSkill(SkillType.FirstAid, 45)
                
                .GrantsFeat(FeatType.CombatEnhancement3);
        }

        private void Shielding()
        {
            _builder.Create(PerkCategoryType.FirstAid, PerkType.Shielding)
                .Name("Shielding")

                .AddPerkLevel()
                .Description("Improves a single target's defense by 5 for 15 minutes. Consumes stim pack on use.")
                .Price(2)
                .RequirementSkill(SkillType.FirstAid, 5)
                
                .GrantsFeat(FeatType.Shielding1)

                .AddPerkLevel()
                .Description("Improves a single target's defense by 10 for 15 minutes. Consumes stim pack on use.")
                .Price(3)
                .RequirementSkill(SkillType.FirstAid, 15)
                
                .GrantsFeat(FeatType.Shielding2)

                .AddPerkLevel()
                .Description("Improves a single target's defense by 15 for 15 minutes. Consumes stim pack on use.")
                .Price(3)
                .RequirementSkill(SkillType.FirstAid, 30)
                
                .GrantsFeat(FeatType.Shielding3)

                .AddPerkLevel()
                .Description("Improves a single target's defense by 20 for 15 minutes. Consumes stim pack on use.")
                .Price(4)
                .RequirementSkill(SkillType.FirstAid, 45)
                
                .GrantsFeat(FeatType.Shielding4);
        }
    }
}
