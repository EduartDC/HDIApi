using HDIApi.DTOs;

namespace HDIApi.Bussines.Interface
{
    public interface IPolicyProvider
    {
        Task<PolicyDTO> GetPolicyByDriver(string idDriver);
    }
}
