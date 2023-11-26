using HDIApi.Bussines.Interface;
using HDIApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDIApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReportProvider _reportProvider;

        public ReportController(IReportProvider reportProvider, ILogger<ReportController> logger)
        {
            _logger = logger;
            _reportProvider = reportProvider;
        }

        [HttpPost("CreateReport")]
        public async Task<IActionResult> CreateReport([FromBody] NewReportDTO report)
        {
            IActionResult result;
            try
            {
                var respond = await _reportProvider.CreateReport(report);
                if (respond == null)
                {
                    result = BadRequest();
                }
                else
                {
                    result = Ok();
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(500, ex.Message);
            }
            return result;
        }
    }
}
