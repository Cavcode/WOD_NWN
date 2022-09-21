﻿using System;
using System.Collections.Generic;
using System.Linq;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Service.LogService;

namespace WOD.Game.Server.Service.PropertyService
{
    public static class StructureChangedAction
    {
        private static readonly Dictionary<StructureType, Dictionary<StructureChangeType, Action<WorldProperty, uint>>> _actions = new();

        /// <summary>
        /// Builds the actions which are run when certain structures are changed in the game world.
        /// </summary>
        /// <returns>A dictionary of spawn actions.</returns>
        public static Dictionary<StructureType, Dictionary<StructureChangeType, Action<WorldProperty, uint>>> BuildSpawnActions()
        {
            // Structure position changed actions
            Assign(StructureType.CityHall, StructureChangeType.PositionChanged, ChangeCityHall());
            Assign(StructureType.Bank, StructureChangeType.PositionChanged, ChangeBank());
            Assign(StructureType.MedicalCenter, StructureChangeType.PositionChanged, ChangeMedicalCenter());
            Assign(StructureType.Starport, StructureChangeType.PositionChanged, ChangeStarport());
            Assign(StructureType.Cantina, StructureChangeType.PositionChanged, ChangeCantina());
            Assign(StructureType.SmallHouseStyle1, StructureChangeType.PositionChanged, ChangeHouse());
            Assign(StructureType.SmallHouseStyle2, StructureChangeType.PositionChanged, ChangeHouse());
            Assign(StructureType.SmallHouseStyle3, StructureChangeType.PositionChanged, ChangeHouse());
            Assign(StructureType.SmallHouseStyle4, StructureChangeType.PositionChanged, ChangeHouse());
            Assign(StructureType.MediumHouseStyle1, StructureChangeType.PositionChanged, ChangeHouse());
            Assign(StructureType.MediumHouseStyle2, StructureChangeType.PositionChanged, ChangeHouse());
            Assign(StructureType.MediumHouseStyle3, StructureChangeType.PositionChanged, ChangeHouse());
            Assign(StructureType.MediumHouseStyle4, StructureChangeType.PositionChanged, ChangeHouse());
            Assign(StructureType.LargeHouseStyle1, StructureChangeType.PositionChanged, ChangeHouse());
            Assign(StructureType.LargeHouseStyle2, StructureChangeType.PositionChanged, ChangeHouse());
            Assign(StructureType.LargeHouseStyle3, StructureChangeType.PositionChanged, ChangeHouse());
            Assign(StructureType.LargeHouseStyle4, StructureChangeType.PositionChanged, ChangeHouse());

            // Structure retrieved actions
            Assign(StructureType.CityHall, StructureChangeType.Retrieved, ClearDoor());
            Assign(StructureType.Bank, StructureChangeType.Retrieved, ClearDoor());
            Assign(StructureType.MedicalCenter, StructureChangeType.Retrieved, ClearDoor());
            Assign(StructureType.Starport, StructureChangeType.Retrieved, RetrieveStarport());
            Assign(StructureType.Cantina, StructureChangeType.Retrieved, ClearDoor());
            Assign(StructureType.SmallHouseStyle1, StructureChangeType.Retrieved, ClearDoor());
            Assign(StructureType.SmallHouseStyle2, StructureChangeType.Retrieved, ClearDoor());
            Assign(StructureType.SmallHouseStyle3, StructureChangeType.Retrieved, ClearDoor());
            Assign(StructureType.SmallHouseStyle4, StructureChangeType.Retrieved, ClearDoor());
            Assign(StructureType.MediumHouseStyle1, StructureChangeType.Retrieved, ClearDoor());
            Assign(StructureType.MediumHouseStyle2, StructureChangeType.Retrieved, ClearDoor());
            Assign(StructureType.MediumHouseStyle3, StructureChangeType.Retrieved, ClearDoor());
            Assign(StructureType.MediumHouseStyle4, StructureChangeType.Retrieved, ClearDoor());
            Assign(StructureType.LargeHouseStyle1, StructureChangeType.Retrieved, ClearDoor());
            Assign(StructureType.LargeHouseStyle2, StructureChangeType.Retrieved, ClearDoor());
            Assign(StructureType.LargeHouseStyle3, StructureChangeType.Retrieved, ClearDoor());
            Assign(StructureType.LargeHouseStyle4, StructureChangeType.Retrieved, ClearDoor());

            return _actions;
        }

        private static void Assign(StructureType structureType, StructureChangeType changeType, Action<WorldProperty, uint> action)
        {
            if (!_actions.ContainsKey(structureType))
                _actions[structureType] = new Dictionary<StructureChangeType, Action<WorldProperty, uint>>();

            _actions[structureType][changeType] = action;
        }

        private static Location GetDoorLocation(uint building, float orientationAdjustment, float sqrtAdjustment)
        {
            var area = GetArea(building);
            var location = GetLocation(building);

            var position = GetPositionFromLocation(location);
            var orientation = GetFacingFromLocation(location);

            orientation = orientation + orientationAdjustment;
            if (orientation > 360.0) 
                orientation -= 360.0f;

            var mod = sqrt(sqrtAdjustment) * sin(orientation);
            position.X += mod;

            mod = sqrt(sqrtAdjustment) * cos(orientation);
            position.Y -= mod;
            var doorLocation = Location(area, position, orientation);
            return doorLocation;
        }

        private static void SpawnDoor(uint building, Location location, string name)
        {
            DestroyDoor(building);
            var door = CreateObject(ObjectType.Placeable, "building_ent1", location);
            SetLocalObject(building, "PROPERTY_DOOR", door);

            Property.AssignPropertyId(door, Property.GetPropertyId(building));
            SetName(door, name);
            AssignExitLocationToInstance(building, GetLocation(door));
        }

        private static void AssignExitLocationToInstance(uint building, Location location)
        {
            var propertyId = Property.GetPropertyId(building);
            var dbBuilding = DB.Get<WorldProperty>(propertyId);

            if (!dbBuilding.ChildPropertyIds.ContainsKey(PropertyChildType.Interior))
                return;

            var instancePropertyId = dbBuilding.ChildPropertyIds[PropertyChildType.Interior].Single();
            var instance = Property.GetRegisteredInstance(instancePropertyId);

            SetLocalLocation(instance.Area, "BUILDING_EXIT_LOCATION", location);
            SetLocalBool(instance.Area, "BUILDING_EXIT_SET", true);
        }

        private static void DestroyDoor(uint building)
        {
            var door = GetLocalObject(building, "PROPERTY_DOOR");
            if (GetIsObjectValid(door))
                DestroyObject(door);
        }

        private static void AdjustBuildingName(WorldProperty property)
        {
            // If the interior has been linked, also update its name.
            var interiorId = property.ChildPropertyIds.ContainsKey(PropertyChildType.Interior) 
                ? property.ChildPropertyIds[PropertyChildType.Interior].SingleOrDefault()
                : null;
            if (!string.IsNullOrWhiteSpace(interiorId))
            {
                var interior = DB.Get<WorldProperty>(interiorId);
                interior.CustomName = property.CustomName;
                DB.Set(interior);

                var instance = Property.GetRegisteredInstance(interiorId);
                SetName(instance.Area, property.CustomName);
            }
        }

        private static Action<WorldProperty, uint> ClearDoor()
        {
            return (property, building) =>
            {
                DestroyDoor(building);
            };
        }

        private static Action<WorldProperty, uint> RetrieveStarport()
        {
            uint GetLandingWaypoint(uint area)
            {
                var referenceObject = GetFirstObjectInArea(area);

                if (GetTag(referenceObject) == "STARSHIP_DOCKPOINT")
                    return referenceObject;

                return GetNearestObjectByTag("STARSHIP_DOCKPOINT", referenceObject);
            }

            return (property, building) =>
            {
                DestroyDoor(building);

                // If a starport is picked up, all of the player ships which are currently docked
                // there need to be relocated back to the last safe NPC dock they visited.
                var interiorId = property.ChildPropertyIds[PropertyChildType.Interior].SingleOrDefault();
                if (string.IsNullOrWhiteSpace(interiorId))
                    return;
            };
        }

        private static Action<WorldProperty, uint> ChangeCityHall()
        {
            return (property, building) =>
            {
                var location = GetDoorLocation(building, 245f, 95f);
                SpawnDoor(building, location, property.CustomName);
                AdjustBuildingName(property);
            };
        }

        private static Action<WorldProperty, uint> ChangeBank()
        {
            return (property, building) =>
            {
                var location = GetDoorLocation(building, 205f, 55f);
                SpawnDoor(building, location, property.CustomName);
                AdjustBuildingName(property);
            };
        }

        private static Action<WorldProperty, uint> ChangeMedicalCenter()
        {
            return (property, building) =>
            {
                var location = GetDoorLocation(building, 312f, 145f);
                SpawnDoor(building, location, property.CustomName);
                AdjustBuildingName(property);
            };
        }

        private static Action<WorldProperty, uint> ChangeStarport()
        {
            return (property, building) =>
            {
                var location = GetDoorLocation(building, 90f, 220f);
                SpawnDoor(building, location, property.CustomName);
                AdjustBuildingName(property);
            };
        }

        private static Action<WorldProperty, uint> ChangeCantina()
        {
            return (property, building) =>
            {
                var location = GetDoorLocation(building, 90f, 50f);
                SpawnDoor(building, location, property.CustomName);
                AdjustBuildingName(property);
            };
        }

        private static Action<WorldProperty, uint> ChangeHouse()
        {
            return (property, building) =>
            {
                var location = GetDoorLocation(building, 198f, 13.0f);
                SpawnDoor(building, location, property.CustomName);
                AdjustBuildingName(property);
            };
        }

    }
}
