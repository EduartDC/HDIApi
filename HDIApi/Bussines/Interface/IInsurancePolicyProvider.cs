using HDIApi.DTOs;

namespace HDIApi.Bussines.Interface
{
    public interface IInsurancePolicyProvider
    {
        Task<bool> RegisterInsurancePolicy(InsurancePolicyDTO insurancePolicyDTO);
    }
}
