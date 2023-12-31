using HDIApi.Bussines.Interface;
using HDIApi.DTOs;
using HDIApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HDIApi.Bussines
{
    public class ReportProvider : IReportProvider
    {
        private readonly InsurancedbContext _context;

        public ReportProvider(InsurancedbContext context)
        {
            string culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            _context = context;

        }
        public Task<NewReportDTO> CreateReport(NewReportDTO report)
        {
            throw new NotImplementedException();
        }

        public Task<NewReportDTO> GetReportByDriver(string idDriver)
        {
            throw new NotImplementedException();
        }

        public async Task<ReportDTO> GetReportById(string idReport)
        {
            var result = new ReportDTO();
            try
            {
                bool canConnect = await _context.Database.CanConnectAsync();

                if (!canConnect)
                {
                    throw new Exception("No se pudo establecer conexión con la base de datos.");
                }
                else
                {
                    var report = _context.Accidents
                        .Include(c=>c.VehicleClientIdVehicleClientNavigation)
                        .Include(c=>c.DriverClientIdDriverClientNavigation)
                        .Include(i=>i.Images)
                        .Include(e=>e.Involveds)
                        .Include(o=>o.OpinionAdjusterIdOpinionAdjusterNavigation)
                        .Where(x => x.IdAccident == idReport)
                        .FirstOrDefault();

                    if (report != null)
                    {
                        var itemDTO = new ReportDTO();
                        itemDTO.IdAccident = report.IdAccident;
                        itemDTO.AccidentDate = report.AccidentDate;
                        itemDTO.IdDriverClient = report.DriverClientIdDriverClient;
                        itemDTO.IdVehicleClient = report.VehicleClientIdVehicleClient;
                        itemDTO.Latitude = report.Latitude;
                        itemDTO.Longitude = report.Longitude;
                        itemDTO.Location = report.Location;
                        itemDTO.NameLocation = report.NameLocation;
                        itemDTO.ReportStatus = report.ReportStatus;
                        itemDTO.IdOpinionAdjuster = report.OpinionAdjusterIdOpinionAdjuster;

                        var vehicle = new VehicleclientDTO();
                        vehicle.IdVehicleClient = report.VehicleClientIdVehicleClientNavigation.IdVehicleClient;
                        vehicle.Brand = report.VehicleClientIdVehicleClientNavigation.Brand;
                        vehicle.Model = report.VehicleClientIdVehicleClientNavigation.Model;
                        vehicle.Color = report.VehicleClientIdVehicleClientNavigation.Color;
                        vehicle.Plate = report.VehicleClientIdVehicleClientNavigation.Plate;
                        vehicle.Year = report.VehicleClientIdVehicleClientNavigation.Year;
                        vehicle.SerialNumber = report.VehicleClientIdVehicleClientNavigation.SerialNumber;
                        vehicle.IdVehicleClient = report.VehicleClientIdVehicleClientNavigation.IdVehicleClient;
                        itemDTO.VehicleClient = vehicle;

                        var driver = new DriverclientDTO();
                        driver.IdDriverClient = report.DriverClientIdDriverClientNavigation.IdDriverClient;
                        driver.NameDriver = report.DriverClientIdDriverClientNavigation.NameDriver;
                        driver.LastNameDriver = report.DriverClientIdDriverClientNavigation.LastNameDriver;
                        driver.LicenseNumber = report.DriverClientIdDriverClientNavigation.LicenseNumber;
                        driver.TelephoneNumber = report.DriverClientIdDriverClientNavigation.TelephoneNumber;
                        itemDTO.DriverClient = driver;

                        var images = new List<ImageDTO>();
                        foreach (var item in report.Images)
                        {
                            var image = new ImageDTO();
                            image.ImageReport = item.ImageReport;
                            images.Add(image);
                        }
                        itemDTO.Images = images;

                        if (report.OpinionAdjusterIdOpinionAdjuster != null)
                        {
                            var opinion = new OpinionadjusterDTO();
                            opinion.CreationDate = (DateTime)report.OpinionAdjusterIdOpinionAdjusterNavigation.CreationDate;
                            opinion.Description = report.OpinionAdjusterIdOpinionAdjusterNavigation.Description;
                            opinion.IdOpinionAdjuster = report.OpinionAdjusterIdOpinionAdjusterNavigation.IdOpinionAdjuster;
                            itemDTO.OpinionAdjuster = opinion;
                        }
                        result = itemDTO;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<(int, List<PreviewReportDTO>)> GetPreviewReportsByEmployee(string idEmployee)
        {
            int code = 0;
            List<PreviewReportDTO> reportsList = new List<PreviewReportDTO>();
            try
            {
                var listTemp = _context.Accidents
                .Include(d => d.DriverClientIdDriverClientNavigation)
                .Where(a=>a.EmployeeIdEmployee.Equals(idEmployee)).ToList();

                foreach(var item in listTemp)
                {
                    PreviewReportDTO temp = new PreviewReportDTO()
                    {
                        NameClient = item.DriverClientIdDriverClientNavigation.NameDriver,
                        ReportNumber = item.IdAccident,
                        StatusReport = item.ReportStatus,
                        ReportDate =  item.AccidentDate.GetValueOrDefault(),
                        Latitude = item.Latitude,
                        Longitude = item.Longitude
                    };
                    reportsList.Add(temp);
                }
                code= 200;
            }catch(Exception)
            {
                code = 500;
            }
            return (code, reportsList);
        }
    }
}

