using WOD.Game.Server.Core;
using WOD.Game.Server.Core.NWScript.Enum;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Enumeration;
using WOD.Game.Server.Feature.GuiDefinition.RefreshEvent;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.GuiService;
using WOD.Game.Server.Service.GuiService.Component;

namespace WOD.Game.Server.Feature.GuiDefinition.ViewModel
{
    internal class PlayerStatusViewModel: GuiViewModelBase<PlayerStatusViewModel, GuiPayloadBase>,
        IGuiRefreshable<PlayerStatusRefreshEvent>
    {
        private int _screenHeight;
        private int _screenWidth;
        private int _screenScale;

        private static readonly GuiColor _hpColor = new GuiColor(139, 0, 0);
        private static readonly GuiColor _stmColor = new GuiColor(0, 104, 0);
        private static readonly GuiColor _ResourceColor = new GuiColor(3, 87, 152);

        private static readonly GuiColor _shieldColor = new GuiColor(3, 87, 152);
        private static readonly GuiColor _hullColor = new GuiColor(139, 0, 0);
        private static readonly GuiColor _capacitorColor = new GuiColor(166, 111, 0);

        public GuiColor Bar1Color
        {
            get => Get<GuiColor>();
            set => Set(value);
        }
        
        public GuiColor Bar2Color
        {
            get => Get<GuiColor>();
            set => Set(value);
        }

        public GuiColor Bar3Color
        {
            get => Get<GuiColor>();
            set => Set(value);
        }

        public string Bar1Label
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Bar2Label
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Bar3Label
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Bar1Value
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Bar2Value
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Bar3Value
        {
            get => Get<string>();
            set => Set(value);
        }

        public float Bar1Progress
        {
            get => Get<float>();
            set => Set(value);
        }
        public float Bar2Progress
        {
            get => Get<float>();
            set => Set(value);
        }
        public float Bar3Progress
        {
            get => Get<float>();
            set => Set(value);
        }

        public GuiRectangle RelativeValuePosition
        {
            get => Get<GuiRectangle>();
            set => Set(value);
        }

        protected override void Initialize(GuiPayloadBase initialPayload)
        {
            _screenHeight = -1;
            _screenScale = -1;
            _screenWidth = -1;
            
            UpdateWidget();
            UpdateAllData();
        }

        private void ToggleLabels(bool isCharacter)
        {
            if (isCharacter)
            {
                Bar1Label = "HP:";
                Bar2Label = "STM:";
                Bar3Label = "Resource:";
            }
            else
            {
                Bar1Label = "SH:";
                Bar2Label = "HL:";
                Bar3Label = "CAP:";
            }
        }

        private void UpdateWidget()
        {
            var screenHeight = GetPlayerDeviceProperty(Player, PlayerDevicePropertyType.GuiHeight);
            var screenWidth = GetPlayerDeviceProperty(Player, PlayerDevicePropertyType.GuiWidth);
            var screenScale = GetPlayerDeviceProperty(Player, PlayerDevicePropertyType.GuiScale);

            if (_screenHeight != screenHeight ||
                _screenWidth != screenWidth ||
                _screenScale != screenScale)
            {
                const float WidgetWidth = 200f;
                const float WidgetHeight = 105f;
                const float XOffset = 255f;
                const float YOffset = 165f;

                var scale = screenScale / 100f;
                var x = screenWidth - XOffset * scale;
                var y = screenHeight - YOffset * scale;

                Geometry = new GuiRectangle(
                    x,
                    y,
                    WidgetWidth,
                    WidgetHeight);

                x = 60f * scale;
                RelativeValuePosition = new GuiRectangle(x, 2f, 110f, 50f);

                _screenHeight = screenHeight;
                _screenWidth = screenWidth;
                _screenScale = screenScale;
            }
        }

        private void UpdateHP()
        {
            var currentHP = GetCurrentHitPoints(Player);
            var maxHP = GetMaxHitPoints(Player);

            Bar1Value = $"{currentHP} / {maxHP}";
            Bar1Progress = maxHP <= 0 ? 0 : (float)currentHP / (float)maxHP;
        }

        private void UpdateResource()
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);
            var currentResource = dbPlayer.Resource;
            var maxResource = Stat.GetMaxResource(Player, dbPlayer);
            if (maxResource <= 0)
                Bar3Progress = (float)currentResource / (float)maxResource;
        }

        private void UpdateSingleData(PlayerStatusRefreshEvent.StatType type)
        {
            ToggleLabels(true);
            Bar1Color = _hpColor;
            Bar2Color = _stmColor;
            Bar3Color = _ResourceColor;

            if (type == PlayerStatusRefreshEvent.StatType.HP)
            {
                UpdateHP();
            }
            else if (type == PlayerStatusRefreshEvent.StatType.Resource)
            {
                UpdateResource();
            }
        }

        private void UpdateAllData()
        {
            ToggleLabels(true);
            Bar1Color = _hpColor;
            Bar2Color = _stmColor;
            Bar3Color = _ResourceColor;
            UpdateHP();
            UpdateResource();
        }

        public void Refresh(PlayerStatusRefreshEvent payload)
        {
            UpdateWidget();
            UpdateSingleData(payload.Type);
        }
    }
}
