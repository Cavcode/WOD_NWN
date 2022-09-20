using System.Linq;
using WOD.Game.Server.Core.NWNX;
using WOD.Game.Server.Core.NWNX.Enum;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.Creature;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Feature.AppearanceDefinition.RacialAppearance;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.MigrationService;

namespace WOD.Game.Server.Feature.MigrationDefinition.PlayerMigration
{
    public class _1_LegacyPlayerMigration: LegacyMigrationBase, IPlayerMigration
    {

        public int Version => 1;
        public void Migrate(uint player)
        {
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);

            AutoLevelUp(player);
            ResetNWNSkills(player);
            ResetSavingThrows(player);
            ResetFeats(player);
            ResetHotBar(player);
            ResetStats(player, dbPlayer);
            ResetAlignment(player);
            StoreRacialAppearance(player, dbPlayer);

            MigrateItems(player);
            MigrateCyborgsToHuman(player);

            DB.Set(dbPlayer);
        }

        private void AutoLevelUp(uint player)
        {
            // Most players are Force characters so we default to that class. This can be changed via the migration UI.
            CreaturePlugin.SetClassByPosition(player, 0, ClassType.ForceSensitive);

            GiveXPToCreature(player, 800000);
            var @class = GetClassByPosition(1, player);

            for (var level = 1; level <= 40; level++)
            {
                LevelUpHenchman(player, @class);
            }

            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Might, 10);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Vitality, 10);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Dexterity, 10);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Intellect, 10);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Willpower, 10);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Social, 10);
        }

        private void ResetFeats(uint player)
        {
            PlayerInitialization.ClearFeats(player);
            PlayerInitialization.GrantBasicFeats(player);
        }

        private void ResetNWNSkills(uint player)
        {
            PlayerInitialization.InitializeSkills(player);
        }

        private void ResetSavingThrows(uint player)
        {
            PlayerInitialization.InitializeSavingThrows(player);
        }

        private void ResetStats(uint player, Player dbPlayer)
        {
            dbPlayer.BAB = 1;
            Stat.AdjustPlayerMaxHP(dbPlayer, player, 70);
            Stat.AdjustPlayerMaxResource(dbPlayer, 10, player);
            CreaturePlugin.SetBaseAttackBonus(player, 1);
            dbPlayer.HP = GetCurrentHitPoints(player);
            dbPlayer.Resource = Stat.GetMaxResource(player, dbPlayer);

            dbPlayer.BaseStats[AbilityType.Might] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Might);
            dbPlayer.BaseStats[AbilityType.Dexterity] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Dexterity);
            dbPlayer.BaseStats[AbilityType.Vitality] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Vitality);
            dbPlayer.BaseStats[AbilityType.Willpower] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Willpower);
            dbPlayer.BaseStats[AbilityType.Intellect] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Intellect);
            dbPlayer.BaseStats[AbilityType.Social] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Social);
        }

        private void ResetHotBar(uint player)
        {
            const int MaxSlots = 35;
            for (var slot = 0; slot <= MaxSlots; slot++)
            {
                PlayerPlugin.SetQuickBarSlot(player, slot, PlayerQuickBarSlot.Empty(QuickBarSlotType.Empty));
            }

            PlayerInitialization.InitializeHotBar(player);
        }

        private void ResetAlignment(uint player)
        {
            PlayerInitialization.AdjustAlignment(player);
        }

        private void StoreRacialAppearance(uint player, Player dbPlayer)
        {
            dbPlayer.OriginalAppearanceType = GetAppearanceType(player);
        }

        private void RemoveItems(uint item)
        {
            string[] resrefsToRemove =
            {
                "tk_omnidye",
                "fist",
                "player_guide",
                "xp_tome_1",
                "xp_tome_2",
                "xp_tome_3",
                "xp_tome_4",
                "refund_tome",
                "slug_shake"
            };

            var resref = GetResRef(item);
            if (resrefsToRemove.Contains(resref))
            {
                DestroyObject(item);
            }
        }

        private void MigrateItems(uint player)
        {
            // Inventory Items
            for (var item = GetFirstItemInInventory(player); GetIsObjectValid(item); item = GetNextItemInInventory(player))
            {
                WipeItemProperties(item);
                Item.MarkLegacyItem(item);
                WipeDescription(item);
                WipeVariables(item);
                CleanItemName(item);
                RemoveItems(item);
            }

            // Equipped Items
            for (var index = 0; index < NumberOfInventorySlots; index++)
            {
                var slot = (InventorySlot)index;
                var item = GetItemInSlot(slot, player);

                // Skip invalid items (empty item slots)
                if (!GetIsObjectValid(item))
                    continue;

                // Skip creature items.
                if (slot == InventorySlot.CreatureLeft ||
                    slot == InventorySlot.CreatureRight ||
                    slot == InventorySlot.CreatureBite ||
                    slot == InventorySlot.CreatureArmor)
                {
                    continue;
                }

                WipeItemProperties(item);
                Item.MarkLegacyItem(item);
                WipeDescription(item);
                WipeVariables(item);
                RemoveItems(item);

                AssignCommand(player, () => ActionUnequipItem(item));
            }
        }

        private void MigrateCyborgsToHuman(uint player)
        {
            if (GetRacialType(player) == RacialType.Cyborg)
            {
                CreaturePlugin.SetRacialType(player, RacialType.Human);
            }
        }
    }
}
