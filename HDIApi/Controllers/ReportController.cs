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

        [HttpGet("GetReportById/{idReport}")]
        public async Task<IActionResult> GetReportById(string idReport)
        {
            IActionResult result;
            try
            {
                var respond = await _reportProvider.GetReportById(idReport);
                if (respond == null)
                {
                    result = NotFound();
                }
                else
                {
                    result = Ok(respond);
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(500, ex.Message);
            }
            return result;
        }


        [HttpGet("GetPreviewReportsByEmployee/{idEmployee}")]
        public async Task<IActionResult> GetPreviewReportsByEmployee(string idEmployee)
        {
            int code = 0;
            List<PreviewReportDTO> list = new List<PreviewReportDTO>();
            try
            {
                (code, list) = await _reportProvider.GetPreviewReportsByEmployee(idEmployee);
                if (code == 200)
                {
                    return Ok(list);
                }
                else
                {
                    return StatusCode(code);
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
        [HttpPost("PostOpinion")]
        public async Task<IActionResult> PostOpinion([FromBody] NewOpinionadjusterDTO opinion){
            IActionResult result;
            try
            {
                var respond = await _reportProvider.PostOpinion(opinion);
                if (respond)
                {
                    result = Ok();
                }
                else
                {
                    result = BadRequest();
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(500, ex.Message);
            }
            return result;
        }

        [HttpPut("PutOpinion")]
        public async Task<IActionResult> PutOpinion([FromBody] NewOpinionadjusterDTO opinion){
            IActionResult result;
            try
            {
                var respond = await _reportProvider.PutOpinion(opinion);
                if (respond)
                {
                    result = Ok();
                }
                else
                {
                    result = BadRequest();
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
