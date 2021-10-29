﻿namespace WOD.Game.Server.Feature.AppearanceDefinition
{
    public interface IAppearanceDefinition
    {
        int[] MaleHeads { get; }
        int[] FemaleHeads { get; }

        int[] Torsos { get; } 
        int[] Pelvis { get; }
        int[] RightBicep { get; }
        int[] RightForearm { get; }
        int[] RightHand { get; }
        int[] RightThigh { get; }
        int[] RightShin { get; }
        int[] LeftBicep { get; }
        int[] LeftForearm { get; }
        int[] LeftHand { get; } 
        int[] LeftThigh { get; }
        int[] LeftShin { get; } 
    }
}
