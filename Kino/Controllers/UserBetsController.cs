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

        public UserBetsController(IBetRepository betRepository)
        {
            this.betRepository = betRepository;
        }

        [HttpGet]
        [Route("GetUserBets")]
        public async Task<ActionResult> GetUserBets(int userId)
        {
            var bets = await betRepository.GetUserBets(userId);
            if(bets.Count() > 0)
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
        public async Task<ActionResult> AddBet([FromBody] Bet bet)
        {
            try
            {
                if(bet != null)
                {
                    var betAdded = await betRepository.AddBet(bet);
                    return StatusCode(StatusCodes.Status201Created, betAdded);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
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
