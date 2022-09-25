using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;

namespace WOD.Game.Server.Feature
{
    public class TlkOverrides
    {
        [NWNEventHandler("mod_load")]
        public static void OverrideTlks()
        {
            OverrideAttributeNames();
            OverrideMenuNames();
            OverrideFeatDescriptions();
            OverrideAttackBonus();
        }

        private static void OverrideAttributeNames()
        {
            SetTlkOverride(131, "Social"); // Charisma
            SetTlkOverride(132, "Vitality"); // Constitution
            SetTlkOverride(133, "Dexterity"); // Dexterity
            SetTlkOverride(134, "Power"); // Intelligence
            SetTlkOverride(135, "Might"); // Strength
            SetTlkOverride(136, "Will"); // Wisdom

            SetTlkOverride(328, "Increased Might By"); // Strength
            SetTlkOverride(329, "Increased Dexterity By"); // Dexterity
            SetTlkOverride(330, "Increased Power By"); // Intelligence
            SetTlkOverride(331, "Increased Vitality By"); // Constitution
            SetTlkOverride(332, "Increased Will By"); // Wisdom
            SetTlkOverride(333, "Increased Social By"); // Charisma

            SetTlkOverride(473, "Might Information"); // Strength
            SetTlkOverride(474, "Dexterity Information"); // Dexterity
            SetTlkOverride(475, "Vitality Information"); // Constitution
            SetTlkOverride(476, "Will Information"); // Wisdom
            SetTlkOverride(477, "Power Information"); // Intelligence
            SetTlkOverride(479, "Social Information"); // Charisma

            SetTlkOverride(457, BuildRecommendedButtonText());

            SetTlkOverride(459, 
                "Might improves damage dealt by melee weapons and increases carrying capacity.\n\n" + 
                "Primary Skills: One-Handed, Two-Handed, Martial Arts, Smithery, Gathering\n\n" +
                "Other Notes:\n\n" +
                "Improves damage and accuracy of regular melee weapons.\n" +
                "Improves damage and accuracy of heavy melee weapons.\n" +
                "Improves damage and accuracy of throwing weapons.");
            SetTlkOverride(460, 
                "Dexterity improves damage dealt by ranged and finesse weapons and increases physical and ranged accuracy.\n\n" +
                "Primary Skills: One-Handed, Two-Handed, Martial Arts, Ranged, Construction, Devices\n\n" + 
                "Other Notes:\n\n" +
                "Improves damage and accuarcy of finesse melee weapons.\n" +
                "Improves evasion.\n" +
                "Improves damage and accuracy of ranged weapons.");
            SetTlkOverride(461, 
                "Vitality improves your max hit points and reduces damage received.\n\n" +
                "Primary Skills: Armor, Smithery, Gunsmithing\n\n" +
                "Other Notes:\n\n" +
                "Increases maximum HP.\n" +
                "Improves physical defense (reducing damage taken).\n");
            SetTlkOverride(462,
                "Will improves your sub-type ability defense and resource pool.\n\n" +
                "Primary Skills: Construction, First Aid\n\n" +
                "Other Notes:\n\n" +
                "Increases maximum resource pool.\n" + 
                "Improves ability defense (reducing damage taken).\n");
            SetTlkOverride(463,
                "Power improves the effectiveness of your sub-type abilities.\n\n" +
                "Primary Skills: Sub-type skills\n\n" +
                "Other Notes:\n\n");
            SetTlkOverride(478,
                "Social improves your XP gain and leadership capabilities.\n\n" +
                "Primary Skills: Leadership\n\n" +
                "Other Notes:\n\n" + 
                "Improves guild point acquisition.\n" +
                "Improves quest credit rewards.\n" + 
                "Improves XP gain.");

            SetTlkOverride(535, "Dollar"); // Gold Piece

            SetTlkOverride(1027, "Poison"); // Acid

            SetTlkOverride(3593, "Give dollars."); // GP
            SetTlkOverride(5025, "The United States dollar, or U.S. dollar, is made up of 100 cents. It is represented by the symbol $ or US$ to differentiate it from other dollar-based currencies. The U.S. dollar is considered a benchmark currency and is the most used currency in transactions across the world."); // GP desc
            SetTlkOverride(6407, "Dollars"); // GP
            SetTlkOverride(7059, "Drop or give dollars, etc.");

            SetTlkOverride(7099, "Evasion");

            SetTlkOverride(8035, "Resting");
            SetTlkOverride(8049, "Horrified");
            SetTlkOverride(8056, "Accuracy Increased");
            SetTlkOverride(8057, "Accuracy Decreased");
            SetTlkOverride(8060, "Defense Increased");
            SetTlkOverride(8061, "Defense Decreased");
            SetTlkOverride(8062, "Evasion Increased");
            SetTlkOverride(8063, "Evasion Decreased");
            SetTlkOverride(8077, "Force Drained");

            SetTlkOverride(58369, "Might Increased");
            SetTlkOverride(58370, "Might Decreased");
            SetTlkOverride(58371, "Dexterity Increased");
            SetTlkOverride(58372, "Dexterity Decreased");
            SetTlkOverride(58373, "Vitality Increased");
            SetTlkOverride(58374, "Vitality Decreased");
            SetTlkOverride(58375, "Power Increased");
            SetTlkOverride(58376, "Power Decreased");
            SetTlkOverride(58377, "Will Increased");
            SetTlkOverride(58378, "Will Decreased");
            SetTlkOverride(58379, "Social Increased");
            SetTlkOverride(58380, "Social Decreased");

            SetTlkOverride(61619, "Sell <CUSTOM0> for <CUSTOM1>cr");
            SetTlkOverride(61620, "Buy <CUSTOM0> for <CUSTOM1>cr");
            SetTlkOverride(62489, "Acquired <CUSTOM0> dollars");
            SetTlkOverride(62490, "Lost <CUSTOM0> dollars");

            SetTlkOverride(66129, "Premonition");

            SetTlkOverride(66751, "News");
            SetTlkOverride(66755, "Comms");

            SetTlkOverride(83393, "Poison"); // Acid
        }

        private static string BuildRecommendedButtonText()
        {
            return "Your character is guided by six core attributes: Might, Vitality, Dexterity, Will, Power, and Social.\n\n" +
                   "Might: Improves damage dealt by melee weapons, increases melee accuracy and increases carrying capacity.\n" +
                   "Vitality: Improves your max hit points and reduces damage received.\n" +
                   "Power: Improves damage dealt by subtype abilities.\n" +
                   "Will: Improves subtype ability defense.\n" +
                   "Dexterity: Improves ranged accuracy and evasion.\n" +
                   "Social: Improves your XP gain and leadership capabilities.\n\n";
        }

        private static void OverrideMenuNames()
        {
            // Journal - List as Quests
            SetTlkOverride(7037, "Jobs");

            // Spell Book - List as Player Guide
            SetTlkOverride(7038, "Player Guide");
        }

        private static void OverrideFeatDescriptions()
        {
            var template = "Name: {0}\n" +
                           "Resource: {1}\n" +
                           "Recast: {3}s\n" +
                           "Description: {4}\n";

            foreach (var (_, detail) in Perk.GetAllPerks())
            {
                var levelOneFeatDescriptionId = -1;
                var levelOneSpellDescriptionId = -1;
                var levelOneDescription = string.Empty;

                foreach (var (level, perkLevel) in detail.PerkLevels)
                {
                    foreach (var feat in perkLevel.GrantedFeats)
                    {
                        if (feat == FeatType.Invalid)
                            continue;
                        if (!int.TryParse(Get2DAString("feat", "DESCRIPTION", (int)feat), out var featDescriptionId))
                            continue;
                        if (!Ability.IsFeatRegistered(feat))
                            continue;

                        var spellDescriptionId = 0;
                        int.TryParse(Get2DAString("feat", "SPELLID", (int)feat), out var spellId);

                        if (spellId > 0)
                        {
                            int.TryParse(Get2DAString("spells", "SpellDesc", spellId), out spellDescriptionId);
                        }

                        var abilityDetail = Ability.GetAbilityDetail(feat);
                        var res = 0;
                        var recast = abilityDetail.RecastDelay?.Invoke(OBJECT_INVALID) ?? 0f;

                        foreach (var requirement in abilityDetail.Requirements)
                        {
                            if (requirement.GetType() == typeof(AbilityRequirementResource))
                            {
                                var req = (AbilityRequirementResource)requirement;
                                res = req.RequiredResource;
                            }
                        }

                        var description = string.Format(template,
                            abilityDetail.Name,
                            res,
                            recast,
                            perkLevel.Description);

                        if (level == 1)
                        {
                            levelOneDescription = description;
                            levelOneFeatDescriptionId = featDescriptionId;
                        }

                        // Update both the feat and the spell descriptions, if applicable
                        SetTlkOverride(featDescriptionId, description);

                        if (spellDescriptionId > 0)
                        {
                            SetTlkOverride(spellDescriptionId, description);
                            levelOneSpellDescriptionId = spellDescriptionId;
                        }
                    }

                    // Some perks only grant one feat and improve the effectiveness of that feat on each level.
                    // For these, we display every perk level on the feat & spell description (if applicable)
                    if (level > 1 && 
                        !string.IsNullOrWhiteSpace(levelOneDescription) &&
                        perkLevel.GrantedFeats.Count <= 0)
                    {
                        levelOneDescription += $"\n\nLevel #{level}: {perkLevel.Description}";
                        SetTlkOverride(levelOneFeatDescriptionId, levelOneDescription);
                        if(levelOneSpellDescriptionId > 0)
                            SetTlkOverride(levelOneSpellDescriptionId, levelOneDescription);
                    }
                }
            }
        }

        private static void OverrideAttackBonus()
        {
            SetTlkOverride(660, "Accuracy and Damage Penalty");
            SetTlkOverride(734, "Accuracy vs. Alignment Group");
            SetTlkOverride(735, "Accuracy");
            SetTlkOverride(736, "Decreased Accuracy");
            SetTlkOverride(737, "Accuracy vs. Racial Group");
            SetTlkOverride(738, "Accuracy vs. Specific Alignment");
            SetTlkOverride(757, "Select a race for this creature. This is used for preferred enemy and/or accuracy bonus vs race calculations.");
            SetTlkOverride(1085, "Accuracy: This property grants an enhancement bonus to attack rolls made with the weapon, but does not improve the damage dealt by the weapon on a successful hit.");
            SetTlkOverride(1086, "Accuracy vs. Monster Type: This property grants an enhancement bonus to attack rolls made with the weapon, but does not improve the damage dealt by the weapon on a successful hit. It grants this bonus only against a specific monster type, such as giants, undead, or shapeshifters.");
            SetTlkOverride(1087, "Accuracy vs. Specific Alignment: This property grants an enhancement bonus to attack rolls made with the weapon, but does not improve the damage dealt by the weapon on a successful hit. It grants this bonus only when used against creatures of a specific alignment, such as chaotic neutral, lawful evil, or neutral good.");
            SetTlkOverride(1088, "Accuracy vs. Alignment Group: This property grants an enhancement bonus to attack rolls made with the weapon, but does not improve the damage dealt by the weapon on a successful hit. It grants this bonus only when used against creatures from a specific alignment group, such as evil or lawful creatures.");
            SetTlkOverride(1458, "Accuracy Penalty: This property inflicts a penalty on the wielder's attack rolls made with the weapon.");
            SetTlkOverride(1460, "Accuracy Penalty: This property inflicts a penalty on the wielder's attack and damage rolls made with the weapon.");
            SetTlkOverride(2227, "Accuracy Bonus");
            SetTlkOverride(5519, "Accuracy Bonus:");
            SetTlkOverride(5520, "Accuracy Bonus vs.:");
            SetTlkOverride(5521, "Accuracy Penalty:");
            SetTlkOverride(8038, "Enemy Accuracy Bonus");
            SetTlkOverride(40557, "Total Base Accuracy Bonus is %d, but must have at least %d");
            SetTlkOverride(58292, "Accuracy bonus: ");
        }

    }
}
