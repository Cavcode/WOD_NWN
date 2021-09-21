using System.Collections.Generic;
using WOD.Game.Server.Enumeration;

namespace WOD.Game.Server.Service.CraftService
{
    public interface IRecipeListDefinition
    {
        public Dictionary<RecipeType, RecipeDetail> BuildRecipes();
    }
}
