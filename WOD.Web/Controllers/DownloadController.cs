﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WOD.Web.Controllers
{
    public class DownloadController: Controller
    {
        private class Download
        {
            public string LocalPath { get; set; }
            public string ContentType { get; set; }
            public string FileName { get; set; }
        }

        private static readonly Dictionary<int, Download> _downloads = new Dictionary<int, Download>
        {
            {1, new Download { LocalPath = "/var/www/WOD_public_files/WOD GUI.rar", ContentType = "application/octet-stream", FileName = "WODGUI.rar"}},
            {2, new Download{ LocalPath = "/var/www/WOD_public_files/WOD Haks.rar", ContentType = "application/octet-stream ", FileName = "WODDevelopmentHaks.rar"} }
        };

        public IActionResult Index(int id)
        {
            if (!_downloads.ContainsKey(id))
            {
                return Content("Download not found for ID " + id);
            }

            var download = _downloads[id];

            if (!System.IO.File.Exists(download.LocalPath))
            {
                return Content("File not found on server for ID " + id);
            }

            return File(System.IO.File.OpenRead(download.LocalPath), download.ContentType, download.FileName);
        }
    }
}
