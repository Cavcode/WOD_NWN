using System;
using WOD.Game.Server.Core.NWNX;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Feature.DialogDefinition;
using WOD.Game.Server.Feature.GuiDefinition.RefreshEvent;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.CombatService;
using WOD.Game.Server.Service.GuiService;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service.SkillService;
using Skill = WOD.Game.Server.Service.Skill;

namespace WOD.Game.Server.Feature.GuiDefinition.ViewModel
{
    public class CharacterSheetViewModel: GuiViewModelBase<CharacterSheetViewModel, GuiPayloadBase>,
        IGuiRefreshable<ChangePortraitRefreshEvent>,
        IGuiRefreshable<SkillXPRefreshEvent>,
        IGuiRefreshable<EquipItemRefreshEvent>,
        IGuiRefreshable<UnequipItemRefreshEvent>,
        IGuiRefreshable<StatusEffectReceivedRefreshEvent>,
        IGuiRefreshable<StatusEffectRemovedRefreshEvent>
    {
        private const int MaxUpgrades = 10;

        public string PortraitResref
        {
            get => Get<string>();
            set => Set(value);
        }

        public string HP
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Resource
        {
            get => Get<string>();
            set => Set(value);
        }
        public string ResourceName
        {
            get => Get<string>();
            set => Set(value);
        }

        public int Might
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Dexterity
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Vitality
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Will
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Power
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Social
        {
            get => Get<int>();
            set => Set(value);
        }

        public string Name
        {
            get => Get<string>();
            set => Set(value);
        }

        public string MainHandDMG
        {
            get => Get<string>();
            set => Set(value);
        }

        public string OffHandDMG
        {
            get => Get<string>();
            set => Set(value);
        }

        public string MainHandTooltip
        {
            get => Get<string>();
            set => Set(value);
        }

        public string OffHandTooltip
        {
            get => Get<string>();
            set => Set(value);
        }

        public int Attack
        {
            get => Get<int>();
            set => Set(value);
        }

        public int DefensePhysical
        {
            get => Get<int>();
            set => Set(value);
        }

        public int DefenseSubAbility
        {
            get => Get<int>();
            set => Set(value);
        }

        public string DefenseElemental
        {
            get => Get<string>();
            set => Set(value);
        }

        public int Accuracy
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Evasion
        {
            get => Get<int>();
            set => Set(value);
        }

        public string CharacterType
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Race
        {
            get => Get<string>();
            set => Set(value);
        }

        public string SP
        {
            get => Get<string>();
            set => Set(value);
        }

        public string AP
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Control
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Craftsmanship
        {
            get => Get<string>();
            set => Set(value);
        }

        public string RebuildTokens
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool IsMightUpgradeAvailable
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsDexterityUpgradeAvailable
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsVitalityUpgradeAvailable
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsWillUpgradeAvailable
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsPowerUpgradeAvailable
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsSocialUpgradeAvailable
        {
            get => Get<bool>();
            set => Set(value);
        }

        public Action OnClickSkills() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Skills);
        };

        public Action OnClickPerks() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Perks);
        };

        public Action OnClickChangePortrait() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.ChangePortrait);
        };

        public Action OnClickQuests() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Quests);
        };

        public Action OnClickRecipes() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Recipes);
        };

        public Action OnClickKeyItems() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.KeyItems);
        };

        public Action OnClickAchievements() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Achievements);
        };

        public Action OnClickNotes() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Notes);
        };

        public Action OnClickOpenTrash() => () =>
        {
            var location = GetLocation(Player);
            var trash = CreateObject(ObjectType.Placeable, "reo_trash_can", location);
            AssignCommand(Player, () => ActionInteractObject(trash));
            DelayCommand(0.2f, () => SetUseableFlag(trash, false));
        };

        public Action OnClickAppearance() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.AppearanceEditor);
        };

        public Action OnClickSettings() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Settings);
        };

        private void UpgradeAttribute(AbilityType ability, string abilityName)
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);
            var isRacial = dbPlayer.RacialStat == AbilityType.Invalid;
            var promptMessage = isRacial
                ? "WARNING: You are about to spend your one-time racial stat bonus. Once spent, this action CANNOT be undone, even with a character rebuild. Are you SURE you want to upgrade this stat?"
                : $"Upgrading your {abilityName} attribute will consume 1 AP. Are you sure you want to upgrade it?";

            ShowModal(promptMessage, () =>
            {
                if (GetResRef(GetArea(Player)) == "char_migration")
                {
                    FloatingTextStringOnCreature($"Stats cannot be upgraded in this area.", Player, false);
                    return;
                }

                playerId = GetObjectUUID(Player);
                dbPlayer = DB.Get<Player>(playerId);
                isRacial = dbPlayer.RacialStat == AbilityType.Invalid;

                // Racial upgrades do not count toward the 10 cap and they don't reduce AP.
                if (!isRacial)
                {
                    if (dbPlayer.UnallocatedAP <= 0)
                    {
                        FloatingTextStringOnCreature("You do not have enough AP to purchase this upgrade.", Player, false);
                        return;
                    }

                    if (dbPlayer.UpgradedStats[ability] >= MaxUpgrades)
                    {
                        FloatingTextStringOnCreature("You cannot upgrade this attribute any further.", Player, false);
                        return;
                    }

                    dbPlayer.UnallocatedAP--;
                    dbPlayer.UpgradedStats[ability]++;
                }
                else
                {
                    dbPlayer.RacialStat = ability;
                }

                CreaturePlugin.ModifyRawAbilityScore(Player, ability, 1);

                DB.Set(dbPlayer);

                FloatingTextStringOnCreature($"Your {abilityName} attribute has increased!", Player, false);
                LoadData();
            });
        }

        public Action OnClickUpgradeMight() => () =>
        {
            UpgradeAttribute(AbilityType.Might, "Might");
        };

        public Action OnClickUpgradeDexterity() => () =>
        {
            UpgradeAttribute(AbilityType.Dexterity, "Dexterity");
        };

        public Action OnClickUpgradeVitality() => () =>
        {
            UpgradeAttribute(AbilityType.Vitality, "Vitality");
        };

        public Action OnClickUpgradeWill() => () =>
        {
            UpgradeAttribute(AbilityType.Will, "Will");
        };

        public Action OnClickUpgradePower() => () =>
        {
            UpgradeAttribute(AbilityType.Power, "Power");
        };

        public Action OnClickUpgradeSocial() => () =>
        {
            UpgradeAttribute(AbilityType.Social, "Social");
        };

        private void RefreshStats(Player dbPlayer)
        {
            var isRacialBonusAvailable = dbPlayer.RacialStat == AbilityType.Invalid;

           ResourceName = dbPlayer.CharacterType == Enumeration.CharacterType.Kindred ? "Vitae" : "Resource";
           
                ResourceName = "Resource";
            HP = GetCurrentHitPoints(Player) + " / " + GetMaxHitPoints(Player);

            Resource = Stat.GetCurrentResource(Player, dbPlayer) + " / " + Stat.GetMaxResource(Player, dbPlayer);

            Name = GetName(Player);
            Might = GetAbilityScore(Player, AbilityType.Might);
            Dexterity = GetAbilityScore(Player, AbilityType.Dexterity);
            Vitality = GetAbilityScore(Player, AbilityType.Vitality);
            Will = GetAbilityScore(Player, AbilityType.Will);
            Power = GetAbilityScore(Player, AbilityType.Power);
            Social = GetAbilityScore(Player, AbilityType.Social);

            IsMightUpgradeAvailable = (dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Might] < MaxUpgrades) || isRacialBonusAvailable;
            IsDexterityUpgradeAvailable = dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Dexterity] < MaxUpgrades || isRacialBonusAvailable;
            IsVitalityUpgradeAvailable = dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Vitality] < MaxUpgrades || isRacialBonusAvailable;
            IsWillUpgradeAvailable = dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Will] < MaxUpgrades || isRacialBonusAvailable;
            IsPowerUpgradeAvailable = dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Power] < MaxUpgrades || isRacialBonusAvailable;
            IsSocialUpgradeAvailable = dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Social] < MaxUpgrades || isRacialBonusAvailable;
        }

        private void RefreshEquipmentStats(Player dbPlayer)
        {
            // Builds a damage estimate using the player's stats as a baseline.
            (string, string) GetCombatInfo( uint item)
            {
                var itemType = GetBaseItemType(item);
                var skill = Skill.GetSkillTypeByBaseItem(itemType);
                var damageAbility = Item.GetWeaponDamageAbilityType(itemType);
                var damageStat = GetAbilityScore(Player, damageAbility);
                var skillRank = dbPlayer.Skills[skill].Rank;
                var dmg = Item.GetDMG(item) + Combat.GetMiscDMGBonus(Player, itemType);
                var dmgText = $"{dmg} DMG";
                var attack = Stat.GetAttack(Player, damageAbility, skill);
                var defense = Stat.CalculateDefense(damageStat, skillRank, 0);
                var (min, max) = Combat.CalculateDamageRange(attack, dmg, damageStat, defense, damageStat, 0);
                var tooltip = $"Est. Damage: {min} - {max}";

                return (dmgText, tooltip);
            }

            var mainHand = GetItemInSlot(InventorySlot.RightHand, Player);
            var offHand = GetItemInSlot(InventorySlot.LeftHand, Player);
            var mainHandType = GetBaseItemType(mainHand);

            if (GetIsObjectValid(mainHand))
            {
                var dmgInfo = GetCombatInfo(mainHand);
                MainHandDMG = dmgInfo.Item1;
                MainHandTooltip = dmgInfo.Item2;
            }
            else
            {
                MainHandDMG = "-";
                MainHandTooltip = "Est. Damage: N/A";
            }

            if (GetIsObjectValid(offHand))
            {
                var dmgInfo = GetCombatInfo(offHand);
                OffHandDMG = dmgInfo.Item1;
                OffHandTooltip = dmgInfo.Item2;
            }
            else
            {
                OffHandDMG = "-";
                OffHandTooltip = "Est. Damage: N/A";
            }
            
            var damageStat = Item.GetWeaponDamageAbilityType(mainHandType);
            var accuracyStatOverride = AbilityType.Invalid;
            
            // Strong Style (Lightsaber)
            if (Item.LightsaberBaseItemTypes.Contains(mainHandType) &&
                Ability.IsAbilityToggled(Player, AbilityToggleType.StrongStyleLightsaber))
            {
                damageStat = AbilityType.Might;
                accuracyStatOverride = AbilityType.Dexterity;
            }
            // Strong Style (Saberstaff)
            if (Item.SaberstaffBaseItemTypes.Contains(mainHandType) &&
                Ability.IsAbilityToggled(Player, AbilityToggleType.StrongStyleSaberstaff))
            {
                damageStat = AbilityType.Might;
                accuracyStatOverride = AbilityType.Dexterity;
            }

            // Flurry Style (Staff)
            if (Item.StaffBaseItemTypes.Contains(mainHandType) && 
                GetHasFeat(FeatType.CrushingStyle, Player))
            {
                damageStat = AbilityType.Dexterity;
                accuracyStatOverride = AbilityType.Power;
            } 
            
            var mainHandSkill = Skill.GetSkillTypeByBaseItem(mainHandType);
            Attack = Stat.GetAttack(Player, damageStat, mainHandSkill);
            DefensePhysical = Stat.GetDefense(Player, CombatDamageType.Physical, AbilityType.Vitality);
            DefenseSubAbility = Stat.GetDefense(Player, CombatDamageType.Force, AbilityType.Will);

            var fireDefense = dbPlayer.Defenses[CombatDamageType.Fire].ToString();
            var poisonDefense = dbPlayer.Defenses[CombatDamageType.Poison].ToString();
            var electricalDefense = dbPlayer.Defenses[CombatDamageType.Electrical].ToString();
            var iceDefense = dbPlayer.Defenses[CombatDamageType.Ice].ToString();
            DefenseElemental = $"{fireDefense}/{poisonDefense}/{electricalDefense}/{iceDefense}";
            Accuracy = Stat.GetAccuracy(Player, mainHand, accuracyStatOverride, SkillType.Invalid);
            Evasion = Stat.GetEvasion(Player, SkillType.Invalid);

            var smithery = dbPlayer.Control.ContainsKey(SkillType.Smithery)
                ? dbPlayer.Control[SkillType.Smithery]
                : 0;
            var Gunsmithing = dbPlayer.Control.ContainsKey(SkillType.Gunsmithing)
                ? dbPlayer.Control[SkillType.Gunsmithing]
                : 0;
            var Construction = dbPlayer.Control.ContainsKey(SkillType.Construction)
                ? dbPlayer.Control[SkillType.Construction]
                : 0;

            Control = $"{smithery}/{Gunsmithing}/{Construction}";

            smithery = dbPlayer.Craftsmanship.ContainsKey(SkillType.Smithery)
                ? dbPlayer.Craftsmanship[SkillType.Smithery]
                : 0;
            Gunsmithing = dbPlayer.Craftsmanship.ContainsKey(SkillType.Gunsmithing)
                ? dbPlayer.Craftsmanship[SkillType.Gunsmithing]
                : 0;
            Construction = dbPlayer.Craftsmanship.ContainsKey(SkillType.Construction)
                ? dbPlayer.Craftsmanship[SkillType.Construction]
                : 0;
            Craftsmanship = $"{smithery}/{Gunsmithing}/{Construction}";
            RebuildTokens = dbPlayer.NumberRebuildsAvailable.ToString();
        }

        private void RefreshAttributes(Player dbPlayer)
        {
            SP = $"{dbPlayer.TotalSPAcquired} / {Skill.SkillCap} ({dbPlayer.UnallocatedSP})";
            AP = $"{dbPlayer.TotalAPAcquired} / {Skill.APCap} ({dbPlayer.UnallocatedAP})";
        }

        private void RefreshPortrait()
        {
            PortraitResref = GetPortraitResRef(Player) + "l";
        }

        private void LoadData()
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);
            CharacterType = dbPlayer.CharacterType == Enumeration.CharacterType.Kindred ? "Standard" : "Force Sensitive";
            Race = GetStringByStrRef(Convert.ToInt32(Get2DAString("racialtypes", "Name", (int)GetRacialType(Player))), GetGender(Player));

            RefreshPortrait();
            RefreshStats(dbPlayer);
            RefreshEquipmentStats(dbPlayer);
            RefreshAttributes(dbPlayer);
        }
        
        protected override void Initialize(GuiPayloadBase initialPayload)
        {
            LoadData();
        }

        public void Refresh(ChangePortraitRefreshEvent payload)
        {
            RefreshPortrait();
        }

        public void Refresh(SkillXPRefreshEvent payload)
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);

            SP = $"{dbPlayer.TotalSPAcquired} / {Skill.SkillCap} ({dbPlayer.UnallocatedSP})";
            AP = $"{dbPlayer.TotalAPAcquired} / {Skill.APCap} ({dbPlayer.UnallocatedAP})";

            RefreshStats(dbPlayer);
        }

        public void Refresh(EquipItemRefreshEvent payload)
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);
            if (dbPlayer == null)
                return;

            RefreshEquipmentStats(dbPlayer);
        }

        public void Refresh(UnequipItemRefreshEvent payload)
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);
            if (dbPlayer == null)
                return;

            RefreshStats(dbPlayer);
            RefreshEquipmentStats(dbPlayer);
        }

        public void Refresh(StatusEffectReceivedRefreshEvent payload)
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);
            if (dbPlayer == null)
                return;

            RefreshStats(dbPlayer);
            RefreshEquipmentStats(dbPlayer);
        }

        public void Refresh(StatusEffectRemovedRefreshEvent payload)
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);
            if (dbPlayer == null)
                return;

            RefreshStats(dbPlayer);
            RefreshEquipmentStats(dbPlayer);
        }
    }
}
