using HDIApi.Bussines.Interface;
using HDIApi.DTOs;
using HDIApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HDIApi.Bussines
{
    public class AccidentProvider : IAccidentProvider
    {
        private readonly InsurancedbContext _context;

        public AccidentProvider(InsurancedbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccidentDTO>> GetAccidentsByClient(string idClientDriver)
        {
            try
            {
                var accidents = await _context.Accidents
                    .Where(a => a.DriverClientIdDriverClient == idClientDriver)
                    .Select(accident => new AccidentDTO
                    {
                        IdAccident = accident.IdAccident,
                        Location = accident.Location,
                        Longitude = accident.Longitude,
                        Latitude = accident.Latitude,
                        NameLocation = accident.NameLocation,
                        ReportStatus = accident.ReportStatus,
                        AccidentDate = accident.AccidentDate,
                        DriverClientIdDriverClient = accident.DriverClientIdDriverClient,
                        EmployeeIdEmployee = accident.EmployeeIdEmployee,
                        OpinionAdjusterIdOpinionAdjuster = accident.OpinionAdjusterIdOpinionAdjuster,
                        VehicleClientIdVehicleClient = accident.VehicleClientIdVehicleClient
                    })
                    .ToListAsync();

                return accidents;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener accidentes por cliente", ex);
            }
        }

        
    }
}
