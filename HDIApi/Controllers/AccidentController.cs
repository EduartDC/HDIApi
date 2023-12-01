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

        
        [HttpGet("GetAccidentsByClient/{idClientDriver}")]
        [ProducesResponseType(typeof(IEnumerable<AccidentDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAccidentsByClient(string idClientDriver)
        {
            IActionResult result;
            try
            {
                var accidentList = await _accidentProvider.GetAccidentsByClient(idClientDriver);
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

    }
}
