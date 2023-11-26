using HDIApi.DTOs;

namespace HDIApi.Bussines.Interface
{
    public interface IPolicyProvider
    {
        Task<List<PolicyDTO>> GetAllPolicyByDriver(string idDriver);
    }
}
