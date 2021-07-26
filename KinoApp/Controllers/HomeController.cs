using KinoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Kino.Models.KinoDBModel;
using KinoApp.Helpers;
using KinoApp.ServiceHelpers.Draws;
using KinoApp.ServiceHelpers;
using Microsoft.AspNetCore.Authorization;

namespace KinoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IServiceMethods serviceMethods;

        public HomeController(ILogger<HomeController> logger, IServiceMethods serviceMethods)
        {
            _logger = logger;
            this.serviceMethods = serviceMethods;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(DrawViewModelDUMMY());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        #region Helper Methods

        private RecentDrawViewModel DrawViewModelDUMMY()
        {
            var recentDraw = new Draw 
            {
                 DrawDateTime = DateTime.Now,
                 DrawId = "891639",
                 KinoBonus = "46",
                 WinningNumbers = "25#41#51#28#22#9#8#3#75#24#55#77#6#69#26#7#62#67#80#46"
            };

            var model = new RecentDrawViewModel(recentDraw.DrawDateTime)
            {
                DrawId = recentDraw.DrawId,
                KinoBonus = int.Parse(recentDraw.KinoBonus)
            };

            var winningNumbers = recentDraw.WinningNumbers.Split("#");
            foreach (var winningNumber in winningNumbers)
            {
                model.WinningNumbers.Add(int.Parse(winningNumber));
            }

            return model;
        }

        private RecentDrawViewModel GetRecentDrawViewModel()
        {
            var recentDraw = serviceMethods.PerformGet<Draw>(DrawServiceEndpoints.GetMostRecentDraw, null, out var result);
            var model = new RecentDrawViewModel
            {
                WinningNumbers = new List<int>(),
                DrawId = recentDraw.DrawId,
                //DrawDateTime = recentDraw.DrawDateTime,
                KinoBonus = int.Parse(recentDraw.KinoBonus)
            };

            var winningNumbers = recentDraw.WinningNumbers.Split("#");
            foreach (var winningNumber in winningNumbers)
            {
                model.WinningNumbers.Add(int.Parse(winningNumber));
            }

            return model;
        }

        #endregion
    }
}
