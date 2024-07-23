using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Kombi.Dashboard.Services;
using Kombi.Dashboard.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kombi.Dashboard.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CidsController : ControllerBase
    {
        private readonly ILogger<CidsController> _logger;
        private readonly ICidsService _cidsService;

        public CidsController(ILogger<CidsController> logger, ICidsService service)
        {
            _logger = logger;
            _cidsService = service;
        }

        [HttpGet("cids")]
        [ProducesResponseType(typeof(IEnumerable<CidsModel>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cids = await _cidsService.GetCids();
                return Ok(cids);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter Cids");
                return StatusCode(500, ex);
            }
        }
    }
}
