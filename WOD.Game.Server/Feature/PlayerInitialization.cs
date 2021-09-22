using System.Collections.Generic;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWNX;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.SkillService;
using Player = WOD.Game.Server.Entity.Player;
using Skill = WOD.Game.Server.Core.NWScript.Enum.Skill;
using static WOD.Game.Server.Core.NWScript.NWScript;
using Race = WOD.Game.Server.Service.Race;

namespace WOD.Game.Server.Feature
{
    public class PlayerInitialization
    {
        /// <summary>
        /// Handles 
        /// </summary>
        [NWNEventHandler("mod_enter")]
        public static void InitializePlayer()
        {
            var player = GetEnteringObject();

            if (!GetIsPC(player) || GetIsDM(player)) return;

            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId) ?? new Player();

            // Already been initialized. Don't do it again.
            if (dbPlayer.Version >= 1) return;

            ClearInventory(player);
            AutoLevelPlayer(player);
            InitializeSkills(player);
            InitializeSavingThrows(player, dbPlayer);
            RemoveNWNSpells(player);
            ClearFeats(player);
            GrantBasicFeats(player);
            InitializeHotBar(player);
            AdjustStats(player, dbPlayer);
            AdjustAlignment(player);
            InitializeLanguages(player, dbPlayer);
            AssignRacialAppearance(player, dbPlayer);
            GiveStartingItems(player);
            AssignCharacterType(player, dbPlayer);
            RegisterDefaultRespawnPoint(dbPlayer);

            DB.Set(playerId, dbPlayer);
        }

        private static void AutoLevelPlayer(uint player)
        {
            // Capture original stats before we level up the player.
            var str = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Might);
            var con = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Vitality);
            var dex = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Perception);
            var @int = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Unused);
            var wis = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Willpower);
            var cha = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Social);

            GiveXPToCreature(player, 800000);
            var @class = GetClassByPosition(1, player);

            for (var level = 1; level <= 40; level++)
            {
                LevelUpHenchman(player, @class);
            }

            // Set stats back to how they were on entry.
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Might, str);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Vitality, con);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Perception, dex);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Unused, @int);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Willpower, wis);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Social, cha);
        }

        /// <summary>
        /// Wipes a player's equipped items and inventory.
        /// </summary>
        /// <param name="player">The player to wipe an inventory for.</param>
        private static void ClearInventory(uint player)
        {
            for (var slot = 0; slot < NumberOfInventorySlots; slot++)
            {
                var item = GetItemInSlot((InventorySlot)slot, player);
                if (!GetIsObjectValid(item)) continue;

                DestroyObject(item);
            }

            var inventory = GetFirstItemInInventory(player);
            while (GetIsObjectValid(inventory))
            {
                DestroyObject(inventory);
                inventory = GetNextItemInInventory(player);
            }

            TakeGoldFromCreature(GetGold(player), player, true);
        }

        /// <summary>
        /// Initializes all player NWN skills to zero.
        /// </summary>
        /// <param name="player">The player to modify</param>
        private static void InitializeSkills(uint player)
        {
            for (var iCurSkill = 1; iCurSkill <= 27; iCurSkill++)
            {
                var skill = (Skill) (iCurSkill - 1);
                CreaturePlugin.SetSkillRank(player, skill, 0);
            }
        }

        /// <summary>
        /// Initializes all player saving throws to zero.
        /// </summary>
        /// <param name="player">The player to modify</param>
        /// <param name="dbPlayer">The database entity</param>
        private static void InitializeSavingThrows(uint player, Player dbPlayer)
        {
            dbPlayer.Fortitude = 0;
            dbPlayer.Reflex = 0;
            dbPlayer.Will = 0;

            CreaturePlugin.SetBaseSavingThrow(player, SavingThrow.Fortitude, 0);
            CreaturePlugin.SetBaseSavingThrow(player, SavingThrow.Will, 0);
            CreaturePlugin.SetBaseSavingThrow(player, SavingThrow.Reflex, 0);
        }

        /// <summary>
        /// Removes all NWN spells from a player.
        /// </summary>
        /// <param name="player">The player to modify.</param>
        private static void RemoveNWNSpells(uint player)
        {
            var @class = GetClassByPosition(1, player);
            for (var index = 0; index <= 255; index++)
            {
                CreaturePlugin.RemoveKnownSpell(player, @class, 0, index);
            }
        }

        private static void ClearFeats(uint player)
        {
            var numberOfFeats = CreaturePlugin.GetFeatCount(player);
            for (var currentFeat = numberOfFeats; currentFeat >= 0; currentFeat--)
            {
                CreaturePlugin.RemoveFeat(player, CreaturePlugin.GetFeatByIndex(player, currentFeat - 1));
            }
        }

        private static void GrantBasicFeats(uint player)
        {

            var @class = GetClassByPosition(1, player);        

            CreaturePlugin.AddFeatByLevel(player, FeatType.ArmorProficiencyLight, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.ArmorProficiencyMedium, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.ArmorProficiencyHeavy, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.ShieldProficiency, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.WeaponProficiencyExotic, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.WeaponProficiencyMartial, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.WeaponProficiencySimple, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.UncannyDodge1, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.ChatCommandTargeter, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.StructureTool, 1);
            if (@class == ClassType.Brujah)
                Perk.UnlockPerkForPlayer(player, Service.PerkService.PerkType.Awe);
                Perk.UnlockPerkForPlayer(player, Service.PerkService.PerkType.DreadGaze);
                Perk.UnlockPerkForPlayer(player, Service.PerkService.PerkType.Entrancement);
                Perk.UnlockPerkForPlayer(player, Service.PerkService.PerkType.Summon);
                Perk.UnlockPerkForPlayer(player, Service.PerkService.PerkType.Majesty);
        }


        private static void InitializeHotBar(uint player)
        {
            var openRestMenu = PlayerQuickBarSlot.UseFeat(FeatType.OpenRestMenu);
            var chatCommandTargeter = PlayerQuickBarSlot.UseFeat(FeatType.ChatCommandTargeter);
            var restAbility = PlayerQuickBarSlot.UseFeat(FeatType.Rest);
            var structureTool = PlayerQuickBarSlot.UseFeat(FeatType.StructureTool);

            PlayerPlugin.SetQuickBarSlot(player, 0, openRestMenu);
            PlayerPlugin.SetQuickBarSlot(player, 1, chatCommandTargeter);
            PlayerPlugin.SetQuickBarSlot(player, 2, restAbility);
            PlayerPlugin.SetQuickBarSlot(player, 3, structureTool);
        }

        /// <summary>
        /// Modifies the player's unallocated SP, version, max HP, and other assorted stats.
        /// </summary>
        /// <param name="player">The player object</param>
        /// <param name="dbPlayer">The player entity.</param>
        private static void AdjustStats(uint player, Player dbPlayer)
        {
            dbPlayer.UnallocatedSP = 10;
            dbPlayer.Version = 1;
            dbPlayer.Name = GetName(player);
            dbPlayer.BAB = 1;
            Stat.AdjustPlayerMaxHP(dbPlayer, player, 40);
            Stat.AdjustPlayerMaxFP(dbPlayer, 10);
            Stat.AdjustPlayerMaxSTM(dbPlayer, 10);
            CreaturePlugin.SetBaseAttackBonus(player, 1);
            dbPlayer.HP = GetCurrentHitPoints(player);
            dbPlayer.FP = Stat.GetMaxFP(player, dbPlayer);
            dbPlayer.Stamina = Stat.GetMaxStamina(player, dbPlayer);

            dbPlayer.BaseStats[AbilityType.Might] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Might);
            dbPlayer.BaseStats[AbilityType.Perception] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Perception);
            dbPlayer.BaseStats[AbilityType.Vitality] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Vitality);
            dbPlayer.BaseStats[AbilityType.Willpower] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Willpower);
            dbPlayer.BaseStats[AbilityType.Unused] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Unused);
            dbPlayer.BaseStats[AbilityType.Social] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Social);
        }

        /// <summary>
        /// Modifies the player's alignment to Neutral/Neutral since we don't use alignment at all here.
        /// </summary>
        /// <param name="player">The player to object.</param>
        private static void AdjustAlignment(uint player)
        {
            CreaturePlugin.SetAlignmentLawChaos(player, 50);
            CreaturePlugin.SetAlignmentGoodEvil(player, 50);
        }

        /// <summary>
        /// Initializes all of the languages for a player based on their racial type.
        /// </summary>
        /// <param name="player">The player object.</param>
        /// <param name="dbPlayer">The player entity.</param>
        private static void InitializeLanguages(uint player, Player dbPlayer)
        {
            var race = GetRacialType(player);
            var languages = new List<SkillType>(new[] { SkillType.English });


            // Fair warning: We're short-circuiting the skill system here.
            // Languages don't level up like normal skills (no stat increases, SP, etc.)
            // So it's safe to simply set the player's rank in the skill to max.
            foreach (var language in languages)
            {
                var skill = Service.Skill.GetSkillDetails(language);
                if (!dbPlayer.Skills.ContainsKey(language))
                    dbPlayer.Skills[language] = new PlayerSkill();

                var level = skill.MaxRank;
                dbPlayer.Skills[language].Rank = level;

                dbPlayer.Skills[language].XP = Service.Skill.GetRequiredXP(level) - 1;
            }
        }

        /// <summary>
        /// Assigns and stores the player's original racial appearance.
        /// This value is primarily used in the space system for switching between space and character modes.
        /// </summary>
        /// <param name="player">The player object</param>
        /// <param name="dbPlayer">The database entity</param>
        private static void AssignRacialAppearance(uint player, Player dbPlayer)
        {
            DelayCommand(0.1f, () =>
            {
                Race.SetDefaultRaceAppearance(player);
            });
            dbPlayer.OriginalAppearanceType = GetAppearanceType(player);
        }

        /// <summary>
        /// Gives the starting items to the player.
        /// </summary>
        /// <param name="player">The player to receive the starting items.</param>
        private static void GiveStartingItems(uint player)
        {
            var item = CreateItemOnObject("survival_knife", player);
            SetName(item, GetName(player) + "'s Survival Knife");
            SetItemCursedFlag(item, true);

            item = CreateItemOnObject("tk_omnidye", player);
            SetItemCursedFlag(item, true);

            GiveGoldToCreature(player, 100);
        }

        /// <summary>
        /// Sets the character type based on their character class.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="dbPlayer">The player entity</param>
        private static void AssignCharacterType(uint player, Player dbPlayer)
        {
            var @class = GetClassByPosition(1, player);

            if (@class == ClassType.Brujah)
                dbPlayer.CharacterType = CharacterType.Brujah;
            else if (@class == ClassType.Tremere)
                dbPlayer.CharacterType = CharacterType.Tremere;
            else if (@class == ClassType.Gangrel)
                dbPlayer.CharacterType = CharacterType.Gangrel;
            else if (@class == ClassType.Malkavian)
                dbPlayer.CharacterType = CharacterType.Malkavian;
            else if (@class == ClassType.Nosferatu)
                dbPlayer.CharacterType = CharacterType.Nosferatu;
            else if (@class == ClassType.Toreador)
                dbPlayer.CharacterType = CharacterType.Toreador;
            else if (@class == ClassType.Ventrue)
                dbPlayer.CharacterType = CharacterType.Ventrue;
        }

        /// <summary>
        /// Sets the player's default respawn location to that of the waypoint with tag 'DTH_DEFAULT_RESPAWN_POINT'.
        /// If no waypoint by that tag can be found, an error will be logged.
        /// </summary>
        /// <param name="dbPlayer">The player's database entity.</param>
        private static void RegisterDefaultRespawnPoint(Player dbPlayer)
        {
            const string DefaultRespawnWaypointTag = "DTH_DEFAULT_RESPAWN_POINT";
            var waypoint = GetWaypointByTag(DefaultRespawnWaypointTag);

            if (!GetIsObjectValid(waypoint))
            {
                Log.Write(LogGroup.Error, $"Default respawn waypoint could not be located. Did you place a waypoint with the tag 'DTH_DEFAULT_RESPAWN_POINT'?");
                return;
            }

            var location = GetLocation(waypoint);
            var position = GetPositionFromLocation(location);
            var orientation = GetFacingFromLocation(location);
            var area = GetAreaFromLocation(location);
            var areaResref = GetResRef(area);

            dbPlayer.RespawnAreaResref = areaResref;
            dbPlayer.RespawnLocationX = position.X;
            dbPlayer.RespawnLocationY = position.Y;
            dbPlayer.RespawnLocationZ = position.Z;
            dbPlayer.RespawnLocationOrientation = orientation;
        }

    }
}
