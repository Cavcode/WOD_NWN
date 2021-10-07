﻿using WOD.Game.Server.Core;
using WOD.Game.Server.Service.GuiService.Component;

namespace WOD.Game.Server.Service.GuiService
{
    public class GuiConstructedWindow
    {
        public GuiWindowType Type { get; set; }
        public string WindowId { get; set; }
        public Json Window { get; set; }
        public CreatePlayerWindowDelegate CreatePlayerWindowAction { get; set; }
        public GuiRectangle InitialGeometry { get; set; }

        public GuiConstructedWindow(
            GuiWindowType type, 
            string windowId, 
            Json window,
            GuiRectangle initialGeometry,
            CreatePlayerWindowDelegate createPlayerWindowAction)
        {
            Type = type;
            WindowId = windowId;
            Window = window;
            InitialGeometry = initialGeometry;
            CreatePlayerWindowAction = createPlayerWindowAction;
        }
    }
}
