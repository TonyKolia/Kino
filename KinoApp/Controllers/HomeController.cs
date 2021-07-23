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

        public IActionResult Index()
        {
            var model = serviceMethods.PerformGet<IEnumerable<Draw>>(DrawServiceEndpoints.GetDraws, null, out var result);

            return View(model);
        }

        public IActionResult Privacy()
        {
            //var draw = new Draw
            //{
            //    DrawDateTime = DateTime.Now,
            //    DrawId = "1234",
            //    KinoBonus = "32",
            //    WinningNumbers = "123"
            //};
            //serviceMethods.PerformPost(DrawServiceEndpoints.AddDraw, null, draw, out var result);

            var user = new User 
            {
                Id = 2,
                Username = "tooooooost",
                 Birthdate = DateTime.Now,
                  Category = 2,
                   Email = "teeeeest",
                    Password = "12312312",
                     RegisterDate = DateTime.Now
            };


            serviceMethods.PerformUpdate("User/UpdateUser", null, user,  out var result);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
