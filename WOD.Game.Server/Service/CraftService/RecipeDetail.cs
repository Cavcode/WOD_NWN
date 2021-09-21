﻿using System.Collections.Generic;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service.SkillService;

namespace WOD.Game.Server.Service.CraftService
{
    public class RecipeDetail
    {
        public int Quantity { get; set; }
        public string Resref { get; set; }
        public List<IRecipeRequirement> Requirements { get; set; }
        public Dictionary<string, int> Components { get; set; }
        public SkillType Skill { get; set; }
        public RecipeCategoryType Category { get; set; }
        public bool IsActive { get; set; }
        public int Level { get; set; }
        public RecipeModType ModType { get; set; }
        public int ModSlots { get; set; }

        public RecipeDetail()
        {
            IsActive = true;
            Quantity = 1;
            Category = RecipeCategoryType.Uncategorized;
            ModType = RecipeModType.None;
            ModSlots = 0;

            Requirements = new List<IRecipeRequirement>();
            Components = new Dictionary<string, int>();
        }
    }
}
