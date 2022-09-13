using System.Collections.Generic;

namespace WOD.Game.Server.Service.PropertyService
{
    internal interface IPropertyLayoutListDefinition
    {
        public Dictionary<PropertyLayoutType, PropertyLayout> Build();
    }
}
