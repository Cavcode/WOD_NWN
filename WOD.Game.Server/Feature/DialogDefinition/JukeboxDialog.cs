﻿using WOD.Game.Server.Service;
using WOD.Game.Server.Service.DialogService;
using static WOD.Game.Server.Core.NWScript.NWScript;

namespace WOD.Game.Server.Feature.DialogDefinition
{
    public class JukeboxDialog: DialogBase
    {
        private const string MainPageId = "MAIN_PAGE";


        public override PlayerDialog SetUp(uint player)
        {
            var builder = new DialogBuilder()
                .AddPage(MainPageId, (page) =>
                {
                    page.Header = "Please select a song.";

                    foreach (var song in Music.GetAllSongs())
                    {
                        page.AddResponse(song.DisplayName, () =>
                        {
                            var area = GetArea(player);
                            FloatingTextStringOnCreature($"Song Selected: {song.DisplayName}", player, false);

                            MusicBackgroundChangeDay(area, song.ID);
                            MusicBackgroundChangeNight(area, song.ID);
                            MusicBackgroundPlay(area);
                        });
                    }

                });

            return builder.Build();
        }
    }
}
