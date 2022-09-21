using WOD.Game.Server.Entity;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.DialogService;
using WOD.Game.Server.Service.PropertyService;

namespace WOD.Game.Server.Feature.DialogDefinition
{
    public class PropertyExitDialog: DialogBase
    {
        private const string MainPageId = "MAIN_PAGE";

        public override PlayerDialog SetUp(uint player)
        {
            var builder = new DialogBuilder()
                .AddPage(MainPageId, MainPageInit);


            return builder.Build();
        }

        private void ReturnToLastDockedPosition(uint player, PropertyLocation propertyLocation)
        {
            var returningArea = string.IsNullOrWhiteSpace(propertyLocation.AreaResref)
                ? Property.GetRegisteredInstance(propertyLocation.InstancePropertyId).Area
                : Area.GetAreaByResref(propertyLocation.AreaResref);
            
            var location = Location(
                returningArea,
                Vector3(propertyLocation.X, propertyLocation.Y, propertyLocation.Z),
                propertyLocation.Orientation);

            AssignCommand(player, () => ActionJumpToLocation(location));
        }

        private void MainPageInit(DialogPage page)
        {
            var player = GetPC();
            var area = GetArea(player);
            var propertyId = Property.GetPropertyId(area);
            var property = DB.Get<WorldProperty>(propertyId);

            page.Header = $"What would you like to do?";

                page.AddResponse("Exit", () =>
                {
                    // Building interiors will have a location set identifying where their doors are located.
                    // Jump to this location if it's set.
                    if (GetLocalBool(area, "BUILDING_EXIT_SET"))
                    {
                        var location = GetLocalLocation(area, "BUILDING_EXIT_LOCATION");
                        AssignCommand(player, () => ActionJumpToLocation(location));
                    }
                    // Otherwise jump the player to their original location.
                    else
                    {
                        Property.JumpToOriginalLocation(player);
                    }
                });

        }
    }
}
