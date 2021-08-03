using Kino.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutcomeController : ControllerBase
    {
        private readonly IOutcomeRepository outcomeRepository;

        public OutcomeController(IOutcomeRepository outcomeRepository)
        {
            this.outcomeRepository = outcomeRepository;
        }

        [HttpGet]
        [Route("GetOutcomes")]
        public async Task<ActionResult> GetOutcomes()
        {
            return Ok(await outcomeRepository.GetOutcomes());
        }
    }
}
