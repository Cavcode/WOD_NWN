using WOD.Game.Server.Entity;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service.SkillService;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.PerkService
{
    /// <summary>
    /// Adds a minimum skill level as a requirement to purchase or activate a perk.
    /// </summary>
    public class PerkSkillRequirement : IPerkRequirement
    {
        private readonly SkillType _type;
        private readonly int _requiredRank;

        public PerkSkillRequirement(SkillType type, int requiredRank)
        {
            _type = type;
            _requiredRank = requiredRank;
        }

        public bool UsedToCalculateEffectiveLevel => true;

        public string CheckRequirements(uint player)
        {
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);
            var skill = dbPlayer.Skills[_type];
            var rank = skill.Rank;

            if (rank >= _requiredRank) return string.Empty;

            return $"Your skill rank is too low. (Your rank is {rank} versus required rank {_requiredRank})";
        }

        public string RequirementText
        {
            get
            {
                var skillDetails = Skill.GetSkillDetails(_type);
                return $"{skillDetails.Name} rank {_requiredRank}";
            }
        }
    }

}
