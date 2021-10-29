using WOD.Game.Server.Service.GuiService;
using WOD.Game.Server.Service.PlayerMarketService;

namespace WOD.Game.Server.Feature.GuiDefinition.Payload
{
    public class MarketPayload: GuiPayloadBase
    {
        public MarketRegionType RegionType { get; set; }

        public MarketPayload(MarketRegionType regionType)
        {
            RegionType = regionType;
        }
    }
}
