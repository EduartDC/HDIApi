using HDIApi.Bussines.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDIApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PolicyController : ControllerBase
    {
        private readonly ILogger<PolicyController> _logger;
        private readonly IPolicyProvider _policyProvider;

        public PolicyController(IPolicyProvider policyProvider, ILogger<PolicyController> logger)
        {
            _logger = logger;
            _policyProvider = policyProvider;
        }
        [Authorize(Roles = "ajustador")]
        [HttpGet("GetPolicyByDriver/{idDriver}")]
        public async Task<IActionResult> GetPolicyByDiver(string idDriver)
        {
            IActionResult result;
            try
            {
                var policyList = await _policyProvider.GetAllPolicyByDriver(idDriver);
                if (!policyList.Any())
                {
                    result = NotFound();
                }
                else
                {
                    result = Ok(policyList);
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
