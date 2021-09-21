using System.Collections.Generic;

namespace WOD.Game.Server.Service.ItemService
{
    public interface IItemListDefinition
    {
        public Dictionary<string, ItemDetail> BuildItems();
    }
}
