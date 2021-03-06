using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Service.SpawnService;

namespace WOD.Game.Server.Feature.SpawnDefinition
{
    public class HutlarSpawnDefinition: ISpawnListDefinition
    {
        public Dictionary<string, SpawnTable> BuildSpawnTables()
        {
            var builder = new SpawnTableBuilder();
            Byysk(builder);
            QionAnimals(builder);

            return builder.Build();
        }

        private void Byysk(SpawnTableBuilder builder)
        {
            builder.Create("HUTLAR_BYYSK", "Byysk")
                .AddSpawn(ObjectType.Creature, "byysk_warrior")
                .WithFrequency(10)
                .RandomlyWalks()

                .AddSpawn(ObjectType.Creature, "byysk_warrior2")
                .WithFrequency(10)
                .RandomlyWalks();
        }

        private void QionAnimals(SpawnTableBuilder builder)
        {
            builder.Create("HUTLAR_QION_ANIMALS", "Qion Animals")
                .AddSpawn(ObjectType.Creature, "qion_slug")
                .WithFrequency(10)
                .RandomlyWalks()

                .AddSpawn(ObjectType.Creature, "qion_tiger")
                .WithFrequency(8)
                .RandomlyWalks();
        }
    }
}
