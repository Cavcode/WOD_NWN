using System.Collections.Generic;
using WOD.Game.Server.Service.SpaceService;

namespace WOD.Game.Server.Feature.SpaceObjectDefinition
{
    public class TestShipSpaceObjectDefinition: ISpaceObjectListDefinition
    {
        private readonly SpaceObjectBuilder _builder = new SpaceObjectBuilder();

        public Dictionary<string, SpaceObjectDetail> BuildSpaceObjects()
        {
            DemoEnemy();

            return _builder.Build();
        }

        private void DemoEnemy()
        {
            _builder.Create("TargetDummy")
                .ItemTag("ShipDeedLightEscort")
                .ShipModule("com_laser_b");
        }

    }
}
