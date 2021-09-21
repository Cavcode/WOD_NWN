﻿using WOD.Game.Server.Entity;
using WOD.Game.Server.Enumeration;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Service.CraftService
{
    public class RecipeUnlockRequirement: IRecipeRequirement
    {
        private readonly RecipeType _recipe;
        public RecipeUnlockRequirement(RecipeType recipe)
        {
            _recipe = recipe;
        }

        public string CheckRequirements(uint player)
        {
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);

            if (!dbPlayer.UnlockedRecipes.ContainsKey(_recipe))
                return "Recipe must be learned.";

            return string.Empty;
        }

        public string RequirementText => "Recipe must be learned.";
    }
}
