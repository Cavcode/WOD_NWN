using System.Collections.Generic;
using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Core.NWScript.Enum.Item;
using WOD.Game.Server.Core.NWScript.Enum.VisualEffect;
using WOD.Game.Server.Core.NWNX;
using WOD.Game.Server.Core.NWNX.Enum;
using System;

namespace WOD.Game.Server.Feature
{
    public static class GunSounds
    {   
        private static List<VisualEffect> _stone = new List<VisualEffect>()
        {
            VisualEffect.Vfx_Imp_Stone1, VisualEffect.Vfx_Imp_Stone2, VisualEffect.Vfx_Imp_Stone3,
            VisualEffect.Vfx_Imp_Stone4, VisualEffect.Vfx_Gun_Miss1, VisualEffect.Vfx_Gun_Miss2,
            VisualEffect.Vfx_Gun_Miss3, VisualEffect.Vfx_Gun_Miss4
        };
        private static List<VisualEffect> _wood = new List<VisualEffect>()
        {
            VisualEffect.Vfx_Imp_Wood1, VisualEffect.Vfx_Imp_Wood2, VisualEffect.Vfx_Imp_Wood3,
            VisualEffect.Vfx_Imp_Wood4, VisualEffect.Vfx_Gun_Miss1, VisualEffect.Vfx_Gun_Miss2,
            VisualEffect.Vfx_Gun_Miss3, VisualEffect.Vfx_Gun_Miss4
        };
        private static List<VisualEffect> _water = new List<VisualEffect>()
        {
            VisualEffect.Vfx_Imp_Water1, VisualEffect.Vfx_Imp_Water2, VisualEffect.Vfx_Imp_Water3,
            VisualEffect.Vfx_Imp_Water4, VisualEffect.Vfx_Gun_Miss1, VisualEffect.Vfx_Gun_Miss2,
            VisualEffect.Vfx_Gun_Miss3, VisualEffect.Vfx_Gun_Miss4
        };
        private static List<VisualEffect> _metal = new List<VisualEffect>()
        {
            VisualEffect.Vfx_Imp_Metal1, VisualEffect.Vfx_Imp_Metal2, VisualEffect.Vfx_Imp_Metal3,
            VisualEffect.Vfx_Imp_Metal4, VisualEffect.Vfx_Gun_Miss1, VisualEffect.Vfx_Gun_Miss2,
            VisualEffect.Vfx_Gun_Miss3, VisualEffect.Vfx_Gun_Miss4
        };
        private static List<VisualEffect> _dirt = new List<VisualEffect>()
        {
            VisualEffect.Vfx_Imp_Dirt1, VisualEffect.Vfx_Imp_Dirt2, VisualEffect.Vfx_Imp_Dirt3,
            VisualEffect.Vfx_Imp_Dirt4, VisualEffect.Vfx_Gun_Miss1, VisualEffect.Vfx_Gun_Miss2,
            VisualEffect.Vfx_Gun_Miss3, VisualEffect.Vfx_Gun_Miss4
        };
        private static List<VisualEffect> _miss = new List<VisualEffect>()
        {
            VisualEffect.Vfx_Gun_Miss1, VisualEffect.Vfx_Gun_Miss2, VisualEffect.Vfx_Gun_Miss3,
            VisualEffect.Vfx_Gun_Miss4
        };
        private static List<VisualEffect> _flesh = new List<VisualEffect>()
        {
            VisualEffect.Vfx_Imp_Flsh1, VisualEffect.Vfx_Imp_Flsh2, VisualEffect.Vfx_Imp_Flsh3,
            VisualEffect.Vfx_Imp_Flsh4
        };

        /// <summary>
        /// When a lightsaber or saberstaff is equipped, play an audio sound of the saber turning on and then apply
        /// an effect which plays the saber humming sound effect.
        /// </summary>
        [NWNEventHandler("on_nwnx_atk")]
        public static void PlaySound()
        {
            var attackData = DamagePlugin.GetAttackEventData();
            var target = (uint)attackData.Target;
            var player = OBJECT_SELF;
            var surfaceMat = GetSurfaceMaterial(GetLocation(target));
            var gunshotMissVfx = GetGunshotMissVfxFromMatType(surfaceMat);

            if (GetWeaponRanged(GetItemInSlot(InventorySlot.RightHand,player)))
            {
                var gunshotVfx =

                GetItemHasItemProperty();
                ApplyEffectToObject(DurationType.Instant, gunshotMissVfx, player);

                var casing = EffectVisualEffect(VisualEffect.Vfx_Pistol_Casing);
                DelayCommand(0.4f, () => ApplyEffectToObject(DurationType.Instant, casing, player));

                if (attackData.AttackResult == 4)
                {
                    var ricochet = EffectVisualEffect(VisualEffect.Vfx_Ricochet);
                    DelayCommand(0.5f, () => ApplyEffectToObject(DurationType.Instant, ricochet, target));

                }
            }

        }
        private static Effect GetGunshotMissVfxFromMatType(int surfaceMat)
        {
            var rnd = new Random();
            var randIndex = 0;
            var gunshotVfx = VisualEffect.Vfx_UI_Cancel;
            switch (surfaceMat)
            {
                case (int)SurfaceMatType.Carpet:
                case (int)SurfaceMatType.Wood:
                    randIndex = rnd.Next(_wood.Count);
                    gunshotVfx = _wood[randIndex];
                    break;
                case (int)SurfaceMatType.Grass:
                case (int)SurfaceMatType.Mud:
                case (int)SurfaceMatType.Snow:
                case (int)SurfaceMatType.Sand:
                case (int)SurfaceMatType.Dirt:
                case (int)SurfaceMatType.Leaves:
                    randIndex = rnd.Next(_dirt.Count);
                    gunshotVfx = _dirt[randIndex];
                    break;
                case (int)SurfaceMatType.Stone:
                case (int)SurfaceMatType.Barebones:
                    randIndex = rnd.Next(_stone.Count);
                    gunshotVfx = _stone[randIndex];
                    break;
                case (int)SurfaceMatType.Metal:
                    randIndex = rnd.Next(_metal.Count);
                    gunshotVfx = _metal[randIndex];
                    break;
                case (int)SurfaceMatType.Water:
                case (int)SurfaceMatType.Swamp:
                    randIndex = rnd.Next(_water.Count);
                    gunshotVfx = _water[randIndex];
                    break;
                default:
                    gunshotVfx = VisualEffect.Vfx_Ricochet;
                    break;
            }

            var gunshotVfxPick = EffectVisualEffect(gunshotVfx);
            return gunshotVfxPick;
        }
        private static VisualEffect GetHitVfx()
        {
            var rnd = new Random();
            var randIndex = rnd.Next(_miss.Count);
            var hitVfx = _miss[randIndex];
            var gunshotHitVfxPick = EffectVisualEffect(gunshotVfx);
            return gunshotHitVfxPick;
        }

    }
}
