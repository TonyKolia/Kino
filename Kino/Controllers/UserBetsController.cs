using Kino.Models;
using Kino.Models.KinoDBModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Kino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBetsController : ControllerBase
    {
        private readonly IBetRepository betRepository;
        private readonly IDrawRepository drawRepository;

        public UserBetsController(IBetRepository betRepository, IDrawRepository drawRepository)
        {
            this.betRepository = betRepository;
            this.drawRepository = drawRepository;
        }

        [HttpGet]
        [Route("GetUserBets")]
        public async Task<ActionResult> GetUserBets(int userId)
        {
            var bets = await betRepository.GetUserBets(userId);
            if (bets.Count() > 0)
            {
                return Ok(bets);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("AddBet")]
        public async Task<ActionResult> AddBet([FromBody] Bet bet, int? numberOfDraws)
        {
            try
            {
                if (bet != null && numberOfDraws.HasValue)
                {
                    var addedBets = new List<Bet>();
                    var lastDraw = await drawRepository.GetMostRecentDraw();
                    var lastDrawId = int.Parse(lastDraw.DrawId);
                    
                    for(var i = 0; i < numberOfDraws; i++)
                    {
                        lastDrawId++;
                        bet.DrawId = lastDrawId.ToString();
                        bet.Id = 0;
                        var betAdded = await betRepository.AddBet(bet);
                        addedBets.Add(betAdded);
                    }

                    return StatusCode(StatusCodes.Status201Created, addedBets);
                }
                else
                {
                    return BadRequest("Empty bet sent or number of draws missing, please try again.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding bet to the database");
            }
        }

        [HttpGet]
        [Route("GetUserBetsByOutcome")]
        public async Task<ActionResult> GetUserBetsByOutcome(int userId, int outcome)
        {
            var bets = await betRepository.GetUserBetsByOutcome(outcome, userId);
            if (bets.Count() > 0)
            {
                return Ok(bets);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetUserBetsByDate")]
        public async Task<ActionResult> GetUserBetsByDate(DateTime? dateFrom, DateTime? dateTo, int userId)
        {
            var bets = await betRepository .GetUserBetsByDate(dateFrom, dateTo, userId);
            if (bets.Count() > 0)
            {
                return Ok(bets);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("UpdateBetOutcome")]
        public async Task<ActionResult> UpdateBetOutcome([FromBody] Bet bet, int outcome)
        {
            try
            {
                var updatedBet = await betRepository.UpdateBetOutcome(bet, outcome);
                return Ok(updatedBet);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating bet to the database");
            }
            
        }
    }
}
