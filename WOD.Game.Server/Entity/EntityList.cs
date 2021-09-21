using System.Collections.Generic;

namespace WOD.Game.Server.Entity
{
    public class EntityList<T> : List<T>
        where T : EntityBase
    {
    }
}