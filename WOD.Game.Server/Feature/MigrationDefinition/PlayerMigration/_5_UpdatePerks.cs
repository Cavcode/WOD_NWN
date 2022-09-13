using WOD.Game.Server.Core.NWNX;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.Item;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.MigrationService;
using WOD.Game.Server.Service.PerkService;

namespace WOD.Game.Server.Feature.MigrationDefinition.PlayerMigration
{
    public class _5_UpdatePerks: IPlayerMigration
    {
        public int Version => 5;
        public void Migrate(uint player)
        {
            var rightHandWeapon = GetItemInSlot(InventorySlot.RightHand, player);

            CreaturePlugin.RemoveFeat(player, FeatType.RapidShot);
            Stat.ApplyAttacksPerRound(player, rightHandWeapon);

            var innerStrength = Perk.GetEffectivePerkLevel(player, PerkType.InnerStrength);
            if (innerStrength > 0)
            {
                // Remove old one which only targeted gloves.
                CreaturePlugin.SetCriticalRangeModifier(player, 0, 0, true, BaseItem.Gloves);

                // Apply new one which targets all weapons
                CreaturePlugin.SetCriticalRangeModifier(player, -innerStrength, 0, true);
            }
        }
    }
}
