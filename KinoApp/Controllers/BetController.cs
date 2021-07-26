using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoApp.Controllers
{
    public class BetController : Controller
    {
        [Authorize]
        [HttpGet]
        public IActionResult NewBet()
        {
            return View("NewBetScreen");
        }

        [Authorize]
        [HttpPost]
        public IActionResult NewBetw()
        {
            return View();
        }


    }
}
