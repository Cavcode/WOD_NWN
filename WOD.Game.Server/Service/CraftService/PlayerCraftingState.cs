using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service.SkillService;

namespace WOD.Game.Server.Service.CraftService
{
    public class PlayerCraftingState
    {
        public RecipeType SelectedRecipe { get; set; }
        public SkillType DeviceSkillType { get; set; }
        public bool IsOpeningMenu { get; set; }
        public bool IsAutoCrafting { get; set; }

        public PlayerCraftingState()
        {
            SelectedRecipe = RecipeType.Invalid;
            DeviceSkillType = SkillType.Invalid;
        }

    }
}
