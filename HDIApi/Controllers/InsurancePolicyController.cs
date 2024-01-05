using HDIApi.Bussines.Interface;
using HDIApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HDIApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsurancePolicyController : ControllerBase
    {
        private readonly ILogger<InsurancePolicyController> _logger;
        private readonly IInsurancePolicyProvider _insurancePolicyProvider;

        public InsurancePolicyController(IInsurancePolicyProvider insurancePolicyProvider, ILogger<InsurancePolicyController> logger)
        {
            _logger = logger;
            _insurancePolicyProvider = insurancePolicyProvider;
        }

        [HttpPost("RegisterInsurancePolicy")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterInsurancePolicy([FromBody] InsurancePolicyDTO insurancePolicyDTO)
        {
            IActionResult result;
            try
            {
                var success = await _insurancePolicyProvider.RegisterInsurancePolicy(insurancePolicyDTO);
                if (success)
                {
                    result = StatusCode(201);
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
