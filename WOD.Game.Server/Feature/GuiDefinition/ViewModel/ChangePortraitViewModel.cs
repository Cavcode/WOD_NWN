﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.GuiService;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature.GuiDefinition.ViewModel
{
    public class ChangePortraitViewModel: GuiViewModelBase<ChangePortraitViewModel, GuiPayloadBase>
    {
        public string ActivePortrait
        {
            get => Get<string>();
            set => Set(value);
        }

        public int ActivePortraitInternalId
        {
            get => Get<int>();
            set
            {
                Set(value);
                ActivePortrait = Cache.GetPortraitResrefByInternalId(value) + "l";
            }
        }

        public int MaximumPortraits
        {
            get => Get<int>();
            set => Set(value);
        }

        private void LoadCurrentPortrait()
        {
            var portraitId = GetPortraitId(Player);
            if (portraitId == PORTRAIT_INVALID)
            {
                portraitId = Cache.GetPortraitInternalId(1);
            }

            ActivePortraitInternalId = Cache.GetPortraitInternalId(portraitId);
        }

        protected override void Initialize(GuiPayloadBase initialPayload)
        {
            MaximumPortraits = Cache.PortraitCount;
            LoadCurrentPortrait();

            WatchOnClient(model => model.ActivePortraitInternalId);
        }

        public Action OnPreviousClick() => () =>
        {
            var newId = ActivePortraitInternalId - 1;

            if (newId < 1)
                newId = Cache.PortraitCount;

            ActivePortraitInternalId = newId;
        };

        public Action OnNextClick() => () =>
        {
            var newId = ActivePortraitInternalId + 1;

            if (newId > Cache.PortraitCount)
                newId = 1;

            ActivePortraitInternalId = newId;
        };

        public Action OnRevertClick() => () =>
        {
            LoadCurrentPortrait();
        };

        public Action OnSaveClick() => () =>
        {
            var portraitId = Cache.GetPortraitByInternalId(ActivePortraitInternalId);
            SetPortraitId(Player, portraitId);
        };

    }
}
