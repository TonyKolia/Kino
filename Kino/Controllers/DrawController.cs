using Kino.API.Models.ServiceResponse;
using Kino.Models;
using Kino.Models.KinoDBModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kino.API.Helpers;

namespace Kino.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrawController : ControllerBase
    {
        private readonly IDrawRepository drawRepository;

        public DrawController(IDrawRepository drawRepository)
        {
            this.drawRepository = drawRepository;
        }

        [HttpPost]
        [Route("AddDraw")]
        public async Task<ActionResult> AddDraw([FromBody] Draw draw)
        {
            try
            {
                var addedDraw = await drawRepository.AddDraw(draw);
                return StatusCode(StatusCodes.Status201Created, addedDraw);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding draw to the database");
            }
        }

        [HttpGet]
        [Route("GetDraw")]
        public async Task<ActionResult> GetDraw(string drawId)
        {
            var draw = await drawRepository.GetDraw(drawId);
            if(draw != null)
            {
                return Ok(draw);
            }
            else
            {
                return NotFound(string.Format("The draw with id {0} does not exist.", drawId));
            }
        }

        [HttpGet]
        [Route("GetDraws")]
        public async Task<ActionResult> GetDraws()
        {
            return Ok(await drawRepository.GetDraws());
        }

        [HttpGet]
        [Route("SearchDraw")]
        public async Task<ActionResult> SearchDraw(string DrawId, DateTime? dateFrom, DateTime? dateTo)
        {
            var draws = await drawRepository.SearchDraw(DrawId, dateFrom, dateTo);
            if(draws.Count() > 0)
            {
                return Ok(draws);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("AddDrawFromOPAP")]
        public IActionResult AddDrawFromOPAP()
        {
            try
            {
                var newDraw = ServiceHelper.GetDrawFromOPAP();
                if(drawRepository.GetDraw(newDraw.DrawId) == null)
                {
                    var addedDraw = drawRepository.AddDraw(newDraw);
                    return StatusCode(StatusCodes.Status201Created, addedDraw);
                }
                else
                {
                    return BadRequest(string.Format("Draw with id = {0} has already been added", newDraw.DrawId));
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding draw to the database");
            }
        }
    }
}
