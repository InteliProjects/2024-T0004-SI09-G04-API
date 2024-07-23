using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Kombi.Dashboard.Services;
using System;
using System.Threading.Tasks;

namespace Kombi.Dashboard.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaudeController : ControllerBase
    {
        private readonly ILogger<SaudeController> _logger;
        private readonly ISaudeService _saudeService;

        public SaudeController(ILogger<SaudeController> logger, ISaudeService saudeService)
        {
            _logger = logger;
            _saudeService = saudeService;
        }

        [HttpGet("certificates/currentmonth/{month}")]
        public async Task<IActionResult> GetCertificatesForCurrentMonth(string month)
        {
            try
            {
                var result = await _saudeService.CalculateCertificatesForCurrentMonth(month);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter atestados para o mês atual: {Month}", month);
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("aggregateddays/{month?}")]
        public async Task<IActionResult> GetAggregatedCertificatesDays(string month = null)
        {
            try
            {
                var data = await _saudeService.GetAggregatedCertificatesDays(month);
                if (data.Any())
                {
                    return Ok(data);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting aggregated certificates days.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("zenklub/average")]
        public async Task<IActionResult> GetZenklubAverage()
        {
            try
            {
                var data = await _saudeService.GetZenklubAverage();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a média de sessões do Zenklub.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("monthlyaverages/days")]
        public async Task<IActionResult> GetMonthlyAverageDaysOff()
        {
            try
            {
                var data = await _saudeService.CalculateMonthlyAverageDaysOff();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a média mensal de dias abonados.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("averages/daysbylocation")]
        public async Task<IActionResult> GetAverageDaysOffByLocation()
        {
            try
            {
                var data = await _saudeService.CalculateAverageDaysOffByLocation();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a média de dias abonados por localização.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("diseases/top")]
        public async Task<IActionResult> GetTopDiseases()
        {
            try
            {
                var data = await _saudeService.GetTopDiseases();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter as principais doenças.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("diseasescauses/top")]
        public async Task<IActionResult> GetTopDiseasesCause()
        {
            try
            {
                var data = await _saudeService.GetTopDiseasesCause();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter as principais causas das doenças.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("roles/affected")]
        public async Task<IActionResult> GetTopAffectedRoles()
        {
            try
            {
                var data = await _saudeService.GetTopAffectedRolesAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter os cargos mais afetados.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("directorates/atestados")]
        public async Task<IActionResult> GetAtestadosByDirectorate()
        {
            try
            {
                var data = await _saudeService.GetAtestadosByDirectorateAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter atestados por diretoria.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("zenklub/sessionsperemployee")]
        public async Task<IActionResult> GetZenklubSessionsPerEmployee()
        {
            try
            {
                var data = await _saudeService.GetZenklubSessionsPerEmployeeAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter sessões do Zenklub por colaborador.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("cidstrends")]
        public async Task<IActionResult> GetCidsTrends()
        {
            try
            {
                var trends = await _saudeService.GetCidsTrendsByMonth();
                return Ok(trends);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter as tendências de CID por mês.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("sessions/certificates")]
        public async Task<IActionResult> GetSessionCertificates()
        {
            try
            {
                var data = await _saudeService.CalculateSessionCertificates();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter atestados por sessão do Zenklub.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

    }
}

