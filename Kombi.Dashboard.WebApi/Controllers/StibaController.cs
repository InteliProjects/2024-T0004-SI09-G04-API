using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Kombi.Dashboard.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kombi.Dashboard.Repository;

namespace Kombi.Dashboard.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StibaController : ControllerBase
    {
        private readonly ILogger<StibaController> _logger;
        private readonly IStibaService _stibaService;

        public StibaController(ILogger<StibaController> logger, IStibaService service)
        {
            _logger = logger;
            _stibaService = service;
        }

        [HttpGet("stiba")]
        [ProducesResponseType(typeof(IEnumerable<StibaModel>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var stiba = await _stibaService.GetStibaAnswers();
                return Ok(stiba);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter dados Stiba");
                return StatusCode(500, ex);
            }
        }
    }
}
