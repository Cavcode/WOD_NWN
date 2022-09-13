using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Feature.StatusEffectDefinition.StatusEffectData;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.StatusEffectService;

namespace WOD.Game.Server.Feature.StatusEffectDefinition
{
    public class FoodStatusEffectDefinition: IStatusEffectListDefinition
    {
        private readonly StatusEffectBuilder _builder = new();

        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            Food();
            
            return _builder.Build();
        }

        private void Food()
        {
            _builder.Create(StatusEffectType.Food)
                .Name("Food")
                .EffectIcon(EffectIconType.Food)
                .GrantAction((source, target, length, data) =>
                {
                    if (data == null)
                        return;

                    var foodEffect = (FoodEffectData)data;

                    if (foodEffect.HP > 0)
                    {
                        var playerId = GetObjectUUID(target);
                        var dbPlayer = DB.Get<Player>(playerId);

                        dbPlayer.TemporaryFoodHP = foodEffect.HP;

                        Stat.AdjustPlayerMaxHP(dbPlayer, target, foodEffect.HP);
                        DB.Set(dbPlayer);
                    }
                })
                .RemoveAction((target, data) =>
                {
                    if (data == null)
                        return;

                    var foodEffect = (FoodEffectData)data;

                    if (foodEffect.HP > 0)
                    {
                        var playerId = GetObjectUUID(target);
                        var dbPlayer = DB.Get<Player>(playerId);

                        dbPlayer.TemporaryFoodHP = 0;

                        Stat.AdjustPlayerMaxHP(dbPlayer, target, -foodEffect.HP);
                        DB.Set(dbPlayer);
                    }
                });
        }
    }
}
