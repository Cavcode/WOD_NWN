using System;
using WOD.Game.Server.Entity;
using WOD.Game.Server.Service;
using WOD.Game.Server.Service.GuiService;
using WOD.Game.Server.Service.GuiService.Component;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature.GuiDefinition.ViewModel
{
    public class FeedBarViewModel : GuiViewModelBase<FeedBarViewModel>
    {

        public GuiBindingList<string> Names
        {
            get => Get<GuiBindingList<string>>();
            set => Set(value);
        }

        public GuiBindingList<string> Descriptions
        {
            get => Get<GuiBindingList<string>>();
            set => Set(value);
        }

        public GuiBindingList<GuiColor> Colors
        {
            get => Get<GuiBindingList<GuiColor>>();
            set => Set(value);
        }

        public GuiBindingList<string> AcquiredDates
        {
            get => Get<GuiBindingList<string>>();
            set => Set(value);
        }

        public FeedBarViewModel()
        {
            Names = new GuiBindingList<string>();
            Descriptions = new GuiBindingList<string>();
            Colors = new GuiBindingList<GuiColor>();
        }

        public GuiBindingList<float> Progresses
        {
            get => Get<GuiBindingList<float>>();
            set => Set(value);
        }

        public Action OnLoadWindow() => () =>
        {
            StartFeed();
        };

        private void StartFeed()
        {
            
        }
    }
}
