﻿using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.KeyItemService;

namespace WOD.Game.Server.Feature
{
    public static class PlaceableScripts
    {
        /// <summary>
        /// When a teleport placeable is used, send the user to the configured waypoint.
        /// Checks are made for required key items, if specified as local variables on the placeable.
        /// </summary>
        [NWNEventHandler("teleport")]
        public static void UseTeleportDevice()
        {
            var user = GetLastUsedBy();

            if (GetIsInCombat(user))
            {
                SendMessageToPC(user, "You are in combat.");
                return;
            }

            var device = OBJECT_SELF;
            var destination = GetLocalString(device, "DESTINATION");
            var vfxId = GetLocalInt(device, "VISUAL_EFFECT");
            var vfx = vfxId > 0 ? (VisualEffect) vfxId : VisualEffect.None;
            var requiredKeyItemId = GetLocalInt(device, "KEY_ITEM_ID");
            var missingKeyItemMessage = GetLocalString(device, "MISSING_KEY_ITEM_MESSAGE");
            if (string.IsNullOrWhiteSpace(missingKeyItemMessage))
                missingKeyItemMessage = "You don't have the necessary key item to access this object.";

            if (requiredKeyItemId > 0)
            {
                var keyItem = (KeyItemType) requiredKeyItemId;

                if (!KeyItem.HasKeyItem(user, keyItem))
                {
                    SendMessageToPC(user, missingKeyItemMessage);

                    return;
                }
            }

            if (vfx != VisualEffect.None)
            {
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(vfx), user);
            }

            var waypoint = GetWaypointByTag(destination);

            if (!GetIsObjectValid(waypoint))
            {
                SendMessageToPC(user, "Cannot locate waypoint. Inform an admin this teleporter is broken.");
                return;
            }

            var location = GetLocation(waypoint);
            AssignCommand(user, () => ActionJumpToLocation(location));
        }

        /// <summary>
        /// Applies a permanent VFX on a placeable or creature on heartbeat, then removes the heartbeat script.
        /// </summary>
        [NWNEventHandler("permanent_vfx")]
        public static void ApplyPermanentVisualEffect()
        {
            var target = OBJECT_SELF;

            var vfxId = GetLocalInt(target, "PERMANENT_VFX_ID");
            var vfx = vfxId > 0 ? (VisualEffect) vfxId : VisualEffect.None;
            
            if (vfx != VisualEffect.None)
            {
                ApplyEffectToObject(DurationType.Permanent, EffectVisualEffect(vfx), target);
            }

            var type = GetObjectType(target);
            if(type == ObjectType.Placeable)
                SetEventScript(target, EventScript.Placeable_OnHeartbeat, string.Empty);
            else if (type == ObjectType.Creature)
                SetEventScript(target, EventScript.Creature_OnHeartbeat, string.Empty);
        }

        /// <summary>
        /// Handles starting a generic conversation when a placeable is clicked or used by a player or DM.
        /// </summary>
        [NWNEventHandler("generic_convo")]
        public static void GenericConversation()
        {
            var placeable = OBJECT_SELF;
            var user = GetObjectType(placeable) == ObjectType.Placeable ? GetLastUsedBy() : GetClickingObject();

            if (!GetIsPC(user) && !GetIsDM(user)) return;

            var conversation = GetLocalString(placeable, "CONVERSATION");
            var target = GetLocalBool(placeable, "TARGET_PC") ? user : placeable;

            if (!string.IsNullOrWhiteSpace(conversation))
            {
                Dialog.StartConversation(user, target, conversation);
            }
            else
            {
                AssignCommand(user, () => ActionStartConversation(target, string.Empty, true, false));
            }
        }
        /// <summary>
        /// Handle sitting on an object.        
        /// </summary>
        [NWNEventHandler("sit")]
        public static void Sit()
        {
            var user = GetLastUsedBy();

            AssignCommand(user, () => ActionSit(OBJECT_SELF));
            if (GetObjectVisualTransform(user, ObjectVisualTransform.Scale) == 1.0) return;

            // Transformed creatures sit at the height of their transform. Normalise them to the height of the chair.
            // We want to take the negative/opposite of their differential from "standard" and divide by 2.  So a 
            // creature at 1.6 scale (0.6 above standard) should be Z-transformed by -0.3.
            float fScale = GetObjectVisualTransform(user, ObjectVisualTransform.Scale) - 1.0f;
            SetObjectVisualTransform(user, ObjectVisualTransform.TranslateZ, (-fScale) / 2.0f);           
        }

        /// <summary>
        /// Whenever a player purchases a rebuild from the training terminal,
        /// make them spend a rebuild token and send them to the rebuild area.
        /// </summary>
        [NWNEventHandler("buy_rebuild")]
        public static void PurchaseRebuild()
        {
            var player = GetPCSpeaker();

            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
            {
                SendMessageToPC(player, $"Only players may use this terminal.");
                return;
            }

            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);

            if (dbPlayer.NumberRebuildsAvailable <= 0)
            {
                SendMessageToPC(player, ColorToken.Red($"You do not have any rebuild tokens."));
                return;
            }

            dbPlayer.NumberRebuildsAvailable--;

            DB.Set(dbPlayer);

            var waypoint = GetWaypointByTag("REBUILD_LANDING");
            var location = GetLocation(waypoint);
            AssignCommand(player, () => ClearAllActions());
            AssignCommand(player, () => JumpToLocation(location));

            SendMessageToPC(player, $"Remaining rebuild tokens: {dbPlayer.NumberRebuildsAvailable}");
        }
    }
}
