using WOD.Game.Server.Entity;
using WOD.Game.Server.Enumeration;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.PerkService
{
    public class PerkCharacterTypeRequirement: IPerkRequirement
    {
        private readonly CharacterType _requiredCharacterType;

        public PerkCharacterTypeRequirement(CharacterType type)
        {
            _requiredCharacterType = type;
        }

        public string CheckRequirements(uint player)
        {
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);

            if (dbPlayer.CharacterType != _requiredCharacterType)
            {
                return $"Only {_requiredCharacterType} character types may access this perk.";
            }

            return string.Empty;
        }

        public string RequirementText => $"Character Type: {_requiredCharacterType}";
    }
}
