using System.Collections.Generic;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.StatusEffectService;

namespace WOD.Game.Server.Feature.StatusEffectDefinition
{
    public class ItemStatusEffectDefinition: IStatusEffectListDefinition
    {
        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            var builder = new StatusEffectBuilder();
            ForcePackEffect(builder);
            
            return builder.Build();
        }

        private void ForcePackEffect(StatusEffectBuilder builder)
        {
            void CreateEffect(string name, int amount)
            {
                builder.Create(StatusEffectType.ForcePack1)
                    .Name(name)
                    .EffectIcon(2) // 2 = Regenerate
                    .TickAction((source, target) =>
                    {
                        Stat.RestoreFP(target, amount);
                    });
            }

            CreateEffect("Force Pack I", 2);
            CreateEffect("Force Pack II", 3);
            CreateEffect("Force Pack III", 4);
            CreateEffect("Force Pack IV", 5);
            CreateEffect("Force Pack V", 6);
        }
    }
}
