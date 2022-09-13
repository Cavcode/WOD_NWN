using System.Collections.Generic;
using WOD.Game.Server.Core.Bioware;
using WOD.Game.Server.Core.NWNX;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.Item;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.CombatService;
using WOD.Game.Server.Service.ItemService;
using WOD.Game.Server.Service.PerkService;
using Player = WOD.Game.Server.Entity.Player;

namespace WOD.Game.Server.Feature.ItemDefinition
{
    public class LightsaberItemDefinition: IItemListDefinition
    {
        private const int MaxNumberOfUpgrades = 4;
        private readonly ItemBuilder _builder = new ItemBuilder();

        public Dictionary<string, ItemDetail> BuildItems()
        {
            UpgradeKit();
            return _builder.Build();
        }

        private int GetLightsaberLevel(uint item)
        {
            return GetLocalInt(item , "LIGHTSABER_UPGRADE_COUNT");
        }

        private void SetLightsaberLevel(uint item, int level)
        {
            SetLocalInt(item, "LIGHTSABER_UPGRADE_COUNT", level);
        }

        private void UpgradeKit()
        {
            _builder.Create("saber_upgrade_kit")
                .Delay(12f)
                .PlaysAnimation(Animation.LoopingGetMid)
                .MaxDistance(0.0f)
                .ValidationAction((user, item, target, location) =>
                {
                    if (!GetIsPC(user) || GetIsDM(user))
                    {
                        return "Only players may use this kit.";
                    }

                    var itemType = GetBaseItemType(target);
                    var numberOfUpgrades = GetLightsaberLevel(target);
                    if (numberOfUpgrades >= MaxNumberOfUpgrades)
                    {
                        return $"Lightsabers may only be upgraded {MaxNumberOfUpgrades} times.";
                    }

                    if (itemType != BaseItem.Lightsaber)
                    {
                        return "Only lightsabers may be upgraded with this kit.";
                    }

                    var playerId = GetObjectUUID(user);
                    var dbPlayer = DB.Get<Player>(playerId);

                    if (dbPlayer.CharacterType != CharacterType.ForceSensitive)
                    {
                        return "Only force sensitive characters may use this kit.";
                    }

                    if (GetItemInSlot(InventorySlot.RightHand, user) == target ||
                        GetItemInSlot(InventorySlot.LeftHand, user) == target)
                    {
                        return "Lightsaber must be unequipped.";
                    }

                    return string.Empty;
                })
                .ApplyAction((user, item, target, location) =>
                {
                    var numberOfUpgrades = GetLightsaberLevel(target) + 1;
                    var dmgItemPropertyId = DetermineDMGValue(numberOfUpgrades);

                    var dmgItemProperty = ItemPropertyCustom(ItemPropertyType.DMG, (int)CombatDamageType.Physical, dmgItemPropertyId);
                    var perkRequirementItemProperty = ItemPropertyCustom(ItemPropertyType.UseLimitationPerk, (int)PerkType.LightsaberProficiency, numberOfUpgrades+1);

                    BiowareXP2.IPSafeAddItemProperty(target, dmgItemProperty, 0.0f, AddItemPropertyPolicy.ReplaceExisting, true, false);
                    BiowareXP2.IPSafeAddItemProperty(target, perkRequirementItemProperty, 0.0f, AddItemPropertyPolicy.ReplaceExisting, true, true);

                    DestroyObject(item);
                    SendMessageToPC(user, $"Your lightsaber has been upgraded to level {numberOfUpgrades+1}.");
                    SetLightsaberLevel(target, numberOfUpgrades);
                });

        }

        private int DetermineDMGValue(int upgradeNumber)
        {
            switch (upgradeNumber)
            {
                case 1:
                    return 12; 
                case 2:
                    return 17; 
                case 3:
                    return 21; 
                case 4:
                    return 26; 
                case 5:
                    return 34; 
                case 6:
                    return 39; 
                case 7:
                    return 42; 
                case 8:
                    return 50; 
                case 9:
                    return 56; 
                default:
                    return 8; 
            }
        }

    }
}
