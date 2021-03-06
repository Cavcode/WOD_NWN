using System;
using WOD.Game.Server.Core.NWNX;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.CombatService;
using WOD.Game.Server.Service.GuiService;
using static WOD.Game.Server.Core.NWScript.NWScript;
using Skill = WOD.Game.Server.Service.Skill;

namespace WOD.Game.Server.Feature.GuiDefinition.ViewModel
{
    public class CharacterSheetViewModel: GuiViewModelBase<CharacterSheetViewModel, GuiPayloadBase>
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
        public string FP
        {
            get => Get<string>();
            set => Set(value);
        }

        public string STM
        {
            get => Get<string>();
            set => Set(value);
        }

        public int Might
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Perception
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Vitality
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Willpower
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

        public int Defense
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

        public bool IsMightUpgradeAvailable
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsPerceptionUpgradeAvailable
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsVitalityUpgradeAvailable
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsWillpowerUpgradeAvailable
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
            CreaturePlugin.ModifyRawAbilityScore(Player, ability, 1);

            DB.Set(playerId, dbPlayer);

            FloatingTextStringOnCreature($"Your {abilityName} attribute has increased!", Player, false);
            LoadData();
        }

        public Action OnClickUpgradeMight() => () =>
        {
            ShowModal($"Upgrading your Might attribute will consume 1 AP. Are you sure you want to upgrade it?", () =>
            {
                UpgradeAttribute(AbilityType.Might, "Might");
            });
        };

        public Action OnClickUpgradePerception() => () =>
        {
            ShowModal($"Upgrading your Perception attribute will consume 1 AP. Are you sure you want to upgrade it?", () =>
            {
                UpgradeAttribute(AbilityType.Perception, "Perception");
            });
        };

        public Action OnClickUpgradeVitality() => () =>
        {
            ShowModal($"Upgrading your Vitality attribute will consume 1 AP. Are you sure you want to upgrade it?", () =>
            {
                UpgradeAttribute(AbilityType.Vitality, "Vitality");
            });
        };

        public Action OnClickUpgradeWillpower() => () =>
        {
            ShowModal($"Upgrading your Willpower attribute will consume 1 AP. Are you sure you want to upgrade it?", () =>
            {
                UpgradeAttribute(AbilityType.Willpower, "Willpower");
            });
        };

        public Action OnClickUpgradeSocial() => () =>
        {
            ShowModal($"Upgrading your Social attribute will consume 1 AP. Are you sure you want to upgrade it?", () =>
            {
                UpgradeAttribute(AbilityType.Social, "Social");
            });
        };

        private void LoadData()
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);
            PortraitResref = GetPortraitResRef(Player) + "l";
            var playerType = GetClassByPosition(1, Player);

            HP = GetCurrentHitPoints(Player) + " / " + GetMaxHitPoints(Player);
            FP = Stat.GetCurrentFP(Player, dbPlayer) + " / " + Stat.GetMaxFP(Player, dbPlayer);
            //STM = Stat.GetCurrentStamina(Player, dbPlayer) + " / " + Stat.GetMaxStamina(Player, dbPlayer);
            Name = GetName(Player);
            Might = GetAbilityScore(Player, AbilityType.Might);
            Perception = GetAbilityScore(Player, AbilityType.Perception);
            Vitality = GetAbilityScore(Player, AbilityType.Vitality);
            Willpower = GetAbilityScore(Player, AbilityType.Willpower);
            Social = GetAbilityScore(Player, AbilityType.Social);
            Defense = Stat.GetDefense(Player, CombatDamageType.Physical);
            Evasion = GetAC(Player);
            switch (playerType)
            {
                case ClassType.Brujah:
                    CharacterType = "Brujah";
                    break;
                case ClassType.Nosferatu:
                    CharacterType = "Nosferatu";
                    break;
                case ClassType.Tremere:
                    CharacterType = "Tremere";
                    break;
                case ClassType.Ventrue:
                    CharacterType = "Ventrue";
                    break;
                case ClassType.Malkavian:
                    CharacterType = "Malkavian";
                    break;
                case ClassType.Toreador:
                    CharacterType = "Toreador";
                    break;

                default:
                    CharacterType = "Brujah";
                    break;
            }
            Race = GetStringByStrRef(Convert.ToInt32(Get2DAString("racialtypes", "Name", (int)GetRacialType(Player))), GetGender(Player));
            SP = $"{dbPlayer.TotalSPAcquired} / {Skill.SkillCap} ({dbPlayer.UnallocatedSP})";
            AP = $"{dbPlayer.TotalAPAcquired} / 30 ({dbPlayer.UnallocatedAP})";
            IsMightUpgradeAvailable = dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Might] < MaxUpgrades;
            IsPerceptionUpgradeAvailable = dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Perception] < MaxUpgrades;
            IsVitalityUpgradeAvailable = dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Vitality] < MaxUpgrades;
            IsWillpowerUpgradeAvailable = dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Willpower] < MaxUpgrades;
            IsSocialUpgradeAvailable = dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Social] < MaxUpgrades;
        }
        
        protected override void Initialize(GuiPayloadBase initialPayload)
        {
            LoadData();
        }
    }
}
