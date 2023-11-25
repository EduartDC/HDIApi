using HDIApi.Bussines.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HDIApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolicyController : ControllerBase
    {
        private readonly ILogger<PolicyController> _logger;
        private readonly IPolicyProvider _policyProvider;

        public PolicyController(IPolicyProvider policyProvider, ILogger<PolicyController> logger)
        {
            _logger = logger;
            _policyProvider = policyProvider;
        }

        [HttpGet("GetPolicyByDriver/{idDriver}")]
        public async Task<IActionResult> GetPolicyByDiver(string idDriver)
        {
            IActionResult result;
            try
            {
                var policy = await _policyProvider.GetPolicyByDriver(idDriver);
                if (policy == null)
                {
                    result = NotFound();
                }
                else
                {
                    result = Ok(policy);
                }
            }
            catch (Exception)
            {
                result = StatusCode(500);
            }
            return result;
        }
    }
}
