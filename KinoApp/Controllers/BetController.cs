using Kino.Models.KinoDBModel;
using KinoApp.Helpers;
using KinoApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using KinoApp.ServiceHelpers;
using KinoApp.ServiceHelpers.Bets;
using Newtonsoft.Json;
using System.Reflection;

namespace KinoApp.Controllers
{
    public class BetController : Controller
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly IServiceMethods serviceMethods;

        public BetController(IHttpContextAccessor httpContext, IServiceMethods serviceMethods)
        {
            this.httpContext = httpContext;
            this.serviceMethods = serviceMethods;
        }

        [Authorize]
        [HttpGet]
        public IActionResult NewBet()
        {
            NewBetViewModel newBetViewModel = null;

            if(TempData["Errors"] != null)
            {
                ViewBag.Errors = TempData["Errors"];
            }

            if(TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            if(TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }

            if(TempData["ModelAsJson"] != null)
            {
                var ViewModelAsJson = TempData["ModelAsJson"] as string;
                newBetViewModel = JsonConvert.DeserializeObject<NewBetViewModel>(ViewModelAsJson);
                if (GeneralHelper.IsObjectEmpty(newBetViewModel))
                {
                    newBetViewModel = null;
                }
            }

            return View("NewBetScreen", newBetViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult NewBet(NewBetViewModel newBetViewModel)
        {
            var result = ValidateNewBet(newBetViewModel);
            if (!result.Success)
            {
                TempData["Errors"] = result.Data;
                TempData["ModelAsJson"] = JsonConvert.SerializeObject(newBetViewModel);
            }
            else
            {
                var newBet = new Bet
                {
                    BetAmount = newBetViewModel.BetAmount,
                    BetDate = DateTime.Now,
                    NoOfNumbers = newBetViewModel.NumberOfSelectedNumbers,
                    SelectedNumbers = newBetViewModel.SelectedNumbers,
                    Outcome = 1,
                    UserId = ((ClaimsIdentity)httpContext.HttpContext.User.Identity).GetUserId()
                };

                serviceMethods.PerformPost(BetServiceEndpoints.AddBet, new[] { newBetViewModel.NumberOfDraws.HasValue ? newBetViewModel.NumberOfDraws.Value.ToString() : "" }, newBet, out var res);
                if (!res.Success)
                {
                    TempData["Error"] = res.Message;
                    TempData["ModelAsJson"] = JsonConvert.SerializeObject(newBetViewModel);
                }
                else
                {
                    TempData["Success"] = "Bet submitted successfully!";
                    return Redirect("~/Bet/NewBet");
                }

            }
            return Redirect("~/Bet/NewBet");
        }


        #region Helper Methods

        private OperationResult ValidateNewBet(NewBetViewModel newBetViewModel)
        {
            var errors = new List<string>();

            if (!newBetViewModel.NumberOfSelectedNumbers.HasValue)
            {
                errors.Add("Please select the number of numbers for the bet.");
            }
            else if(newBetViewModel.NumberOfSelectedNumbers.Value < 1 || newBetViewModel.NumberOfSelectedNumbers.Value > 12)
            {
                errors.Add("The number of selected number must be between 1 and 12.");
            }

            if(!newBetViewModel.NumberOfDraws.HasValue)
            {
                errors.Add("Please select the number of draws.");
            }
            else if (newBetViewModel.NumberOfDraws.Value < 1 || newBetViewModel.NumberOfDraws.Value > 3)
            {
                errors.Add("The number of draws must be between 1 and 3.");
            }

            if (!newBetViewModel.BetAmount.HasValue)
            {
                errors.Add("Please select a bet amount.");
            }

            //TODO: Add decimal validation in allowed values

            if (string.IsNullOrEmpty(newBetViewModel.SelectedNumbers))
            {
                errors.Add("No numbers have been selected.");
            }
            else
            {
                var selectedNumbers = newBetViewModel.SelectedNumbers.Split("#");
                var numbersOk = true;
                foreach(var number in selectedNumbers)
                {
                    var success = int.TryParse(number, out var n);
                    if (!success)
                    {
                        errors.Add("Invalid character submited as a number.");
                        numbersOk = false;
                        break;
                    }
                    else if(n < 1 || n > 80)
                    {
                        errors.Add("Selected numbers must be between 1 and 80.");
                        numbersOk = false;
                        break;
                    }
                }

                if (numbersOk)
                {
                    if(newBetViewModel.NumberOfSelectedNumbers.HasValue && newBetViewModel.NumberOfSelectedNumbers.Value > selectedNumbers.Length)
                    {
                        errors.Add(string.Format("You have selected less numbers ({0}) than you declared ({1}).", selectedNumbers.Length, newBetViewModel.NumberOfSelectedNumbers.Value));
                    }

                    if (newBetViewModel.NumberOfSelectedNumbers.HasValue && newBetViewModel.NumberOfSelectedNumbers.Value < selectedNumbers.Length)
                    {
                        errors.Add(string.Format("You have selected more numbers ({0}) than you declared ({1}).", selectedNumbers.Length, newBetViewModel.NumberOfSelectedNumbers.Value));
                    }
                }
            }

            return new OperationResult 
            {
                Success = errors.Count() == 0,
                Data = errors
            };
        }



        #endregion

    }
}
