using HDIApi.Bussines.Interface;
using HDIApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HDIApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AccidentController : ControllerBase
    {
        private readonly ILogger<AccidentController> _logger;
        private readonly IAccidentProvider _accidentProvider;

        public AccidentController(IAccidentProvider accidentProvider, ILogger<AccidentController> logger)
        {
            _logger = logger;
            _accidentProvider = accidentProvider;
        }

        
        [HttpGet("GetAccidents")]
        [ProducesResponseType(typeof(IEnumerable<AccidentDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAccidents()
        {
            IActionResult result;
            try
            {
                var accidentList = await _accidentProvider.GetAccidents();
                if (!accidentList.Any())
                {
                    result = NotFound();
                }
                else
                {
                    result = Ok(accidentList);
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(500, ex.Message);
            }
            return result;
        }

        [HttpGet("GetAccidentsWithoutAdjuster")]
        [ProducesResponseType(typeof(IEnumerable<AccidentDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAccidentsWithoutAdjuster()
        {
            IActionResult result;
            try
            {
                var accidentList = await _accidentProvider.GetAccidentsWithoutAdjuster();
                if (!accidentList.Any())
                {
                    result = NotFound();
                }
                else
                {
                    result = Ok(accidentList);
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(500, ex.Message);
            }
            return result;
        }

        [HttpPost("AssignAdjusterToAccident")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignAdjusterToAccident([FromBody] AdjusterWithAccidentDTO dataDTO)
        {
            IActionResult result;
            try
            {
                var success = await _accidentProvider.AssignAdjusterToAccident(dataDTO.IdAccident, dataDTO.IdAdjuster);

                if (success)
                {
                    result = Ok();
                }
                else
                {
                    result = NotFound();
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
