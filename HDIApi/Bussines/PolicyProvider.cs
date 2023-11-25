using HDIApi.Bussines.Interface;
using HDIApi.DTOs;

namespace HDIApi.Bussines
{
    public class PolicyProvider : IPolicyProvider
    {
        public Task<PolicyDTO> GetPolicyByDriver(string idDriver)
        {
            throw new NotImplementedException();
        }
    }
}
