using WOD.Game.Server.Core.NWNX;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.AbilityService;
using WOD.Game.Server.Service.MigrationService;

namespace WOD.Game.Server.Feature.MigrationDefinition.PlayerMigration
{
    public class _3_AdjustPlayerSpeeds: IPlayerMigration
    {
        public int Version => 3;
        public void Migrate(uint player)
        {
            Ability.ToggleAbility(player, AbilityToggleType.Dash, false);

            CreaturePlugin.SetMovementRate(player, MovementRate.PC);
            CreaturePlugin.SetMovementRateFactor(player, 1.0f);
        }
    }
}
