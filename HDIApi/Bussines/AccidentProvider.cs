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

        public async Task<IEnumerable<AccidentDTO>> GetAccidents()
        {
            try
            {
                var accidents = await _context.Accidents
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
                throw new Exception("Error al obtener accidentes", ex);
            }
        }

        public async Task<IEnumerable<AccidentDTO>> GetAccidentsWithoutAdjuster()
        {
            try
            {
                var accidents = await _context.Accidents
                    .Where(a => string.IsNullOrEmpty(a.EmployeeIdEmployee))
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
                throw new Exception("Error al obtener accidentes sin Ajustador", ex);
            }
        }

        public async Task<bool> AssignAdjusterToAccident(string accidentId, string employeeId)
        {
            try
            {
                var accident = await _context.Accidents.FindAsync(accidentId);

                if (accident == null)
                {
                    return false;
                }

                accident.EmployeeIdEmployee = employeeId;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asignar el Ajustador al accidente", ex);
            }
        }

    }
}
