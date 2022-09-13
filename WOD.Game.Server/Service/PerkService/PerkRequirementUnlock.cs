﻿using WOD.Game.Server.Entity;

namespace WOD.Game.Server.Service.PerkService
{
    public class PerkRequirementUnlock: IPerkRequirement
    {
        private readonly PerkType _perkType;

        public PerkRequirementUnlock(PerkType perkType)
        {
            _perkType = perkType;
        }

        public string CheckRequirements(uint player)
        {
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);
            return !dbPlayer.UnlockedPerks.ContainsKey(_perkType) 
                ? "Perk has not been unlocked yet." 
                : string.Empty;
        }

        public string RequirementText => "Perk must be unlocked.";
    }
}
