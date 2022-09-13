using System.Collections.Generic;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Service.StatusEffectService;

namespace WOD.Game.Server.Feature.StatusEffectDefinition
{
    public class IronShellStatusEffectDefinition: IStatusEffectListDefinition
    {
        private readonly StatusEffectBuilder _builder = new StatusEffectBuilder();

        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            IronShell();

            return _builder.Build();
        }

        private void IronShell()
        {
            _builder.Create(StatusEffectType.IronShell)
                .Name("Iron Shell")
                .EffectIcon(EffectIconType.ElementalShield);
        }
    }
}
