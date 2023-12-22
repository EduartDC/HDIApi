using HDIApi.Bussines.Interface;
using HDIApi.DTOs;
using HDIApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HDIApi.Bussines
{
    public class PolicyProvider : IPolicyProvider
    {
        private readonly InsurancedbContext _context;

        public PolicyProvider(InsurancedbContext context)
        {
            string culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            _context = context;

        }

        public async Task<List<PolicyDTO>> GetAllPolicyByDriver(string idDriver)
        {
            var result = new List<PolicyDTO>();
            try
            {
                bool canConnect = await _context.Database.CanConnectAsync();

                if (!canConnect)
                {
                    throw new Exception("No se pudo establecer conexión con la base de datos.");
                }
                else
                {
                    var policy = _context.Insurancepolicies
                        .Include(c => c.VehicleClientIdVehicleClientNavigation)
                        .Where(x => x.DriverClientIdDriverClient == idDriver).ToList();

                    if (policy.Any())
                    {
                        
                        foreach(var item in policy)
                        {
                            var itemDTO = new PolicyDTO();
                            itemDTO.IdInsurancePolicy = item.IdInsurancePolicy;
                            itemDTO.StartTerm = item.StartTerm;
                            itemDTO.EndTerm = item.EndTerm;
                            itemDTO.TermAmount = item.TermAmount;
                            itemDTO.Price = item.Price;
                            itemDTO.PolicyType = item.PolicyType;
                            itemDTO.IdDriverClient = item.DriverClientIdDriverClient;
                            itemDTO.IdVehicleClient = item.VehicleClientIdVehicleClient;
                            var vehicle = new VehicleclientDTO();
                            vehicle.IdVehicleClient = item.VehicleClientIdVehicleClientNavigation.IdVehicleClient;
                            vehicle.Brand = item.VehicleClientIdVehicleClientNavigation.Brand;
                            vehicle.Model = item.VehicleClientIdVehicleClientNavigation.Model;
                            vehicle.Color = item.VehicleClientIdVehicleClientNavigation.Color;
                            vehicle.Plate = item.VehicleClientIdVehicleClientNavigation.Plate;
                            vehicle.Year = item.VehicleClientIdVehicleClientNavigation.Year;
                            vehicle.SerialNumber = item.VehicleClientIdVehicleClientNavigation.SerialNumber;
                            vehicle.IdInsurancePolicy = item.IdInsurancePolicy;
                            itemDTO.VehicleClient = vehicle;
                            result.Add(itemDTO);
                        }
                    }
                    
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
