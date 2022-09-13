﻿using System.Collections.Generic;
using WOD.Game.Server.Service.SpaceService;

namespace WOD.Game.Server.Feature.SpaceObjectDefinition
{
    public class AsteroidSpaceObjectDefinition : ISpaceObjectListDefinition
    {
        private readonly SpaceObjectBuilder _builder = new();

        public Dictionary<string, SpaceObjectDetail> BuildSpaceObjects()
        {
            Asteroid();

            return _builder.Build();
        }

        private void Asteroid()
        {
            _builder.Create("spc_asteroid")
                .ItemTag("asteroid");
        }

    }
}