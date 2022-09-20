using WOD.Game.Server.Service.StatusEffectService;

namespace WOD.Game.Server.Service.AbilityService
{
    /// <summary>
    /// Adds an Resource requirement to activate a perk.
    /// </summary>
    public class AbilityRequirementResource : IAbilityActivationRequirement
    {
        public int RequiredResource { get; }

        public AbilityRequirementResource(int requiredResource)
        {
            RequiredResource = requiredResource;
        }

        public string CheckRequirements(uint player)
        {
            // DMs are assumed to be able to activate.
            if (GetIsDM(player)) return string.Empty;

            var Resource = Stat.GetCurrentResource(player);

            if (Resource >= RequiredResource) return string.Empty;
            return $"Not enough Resource. (Required: {RequiredResource})";
        }

        public void AfterActivationAction(uint player)
        {
            if (GetIsDM(player)) return;

            // Force Attunement reduces Resource costs to zero.
            if (StatusEffect.HasStatusEffect(player, StatusEffectType.ForceAttunement)) return;

            Stat.ReduceResource(player, RequiredResource);
        }
    }
}
