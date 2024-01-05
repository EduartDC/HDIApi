using HDIApi.Bussines.Interface;
using HDIApi.DTOs;
using HDIApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace HDIApi.Bussines
{
    public class InsurancePolicyProvider : IInsurancePolicyProvider
    {
        private readonly InsurancedbContext _context;

        public InsurancePolicyProvider(InsurancedbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateUniqueInsurancePolicyId()
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
                return BitConverter.ToString(hashedBytes).Replace("-", "").Substring(0, 16);
            }
        }

        public async Task<bool> RegisterInsurancePolicy(InsurancePolicyDTO insurancePolicyDTO)
        {
            try
            {

                var insurancePolicy = new Insurancepolicy
                {
                    IdInsurancePolicy = await GenerateUniqueInsurancePolicyId(),
                    StartTerm = insurancePolicyDTO.StartTerm,
                    EndTerm = insurancePolicyDTO.EndTerm,
                    TermAmount = (int?)insurancePolicyDTO.TermAmount,
                    Price = (float?)insurancePolicyDTO.Price,
                    PolicyType = insurancePolicyDTO.PolicyType,
                    DriverClientIdDriverClient = insurancePolicyDTO.DriverClientIdDriverClient,
                    VehicleClientIdVehicleClient = insurancePolicyDTO.VehicleClientIdVehicleClient
                };

                _context.Add(insurancePolicy);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la póliza de seguros", ex);
            }
        }
    }
}
