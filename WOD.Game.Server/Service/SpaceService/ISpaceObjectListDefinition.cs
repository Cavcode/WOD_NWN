using System.Collections.Generic;

namespace WOD.Game.Server.Service.SpaceService
{
    public interface ISpaceObjectListDefinition
    {
        public Dictionary<string, SpaceObjectDetail> BuildSpaceObjects();
    }
}
