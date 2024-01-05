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

        public async Task<bool> CreateReport(NewReportDTO report)
        {
            var result = false;
            try
            {
                bool canConnect = await _context.Database.CanConnectAsync();

                if (!canConnect)
                {
                    throw new Exception("No se pudo establecer conexión con la base de datos.");
                }
                else
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            var newreport = new Accident();
                            newreport.IdAccident = Guid.NewGuid().ToString();
                            newreport.AccidentDate = DateTime.Now;
                            newreport.ReportStatus = "Pendiente";
                            newreport.DriverClientIdDriverClient = report.IdDriverClient;
                            newreport.VehicleClientIdVehicleClient = report.IdVehicleClient;
                            newreport.Latitude = report.Latitude;
                            newreport.Longitude = report.Longitude;
                            newreport.Location = report.Location;

                            _context.Accidents.Add(newreport);
                            _context.SaveChanges();

                            var images = new List<Image>();
                            foreach (var item in report.Images)
                            {
                                var image = new Image();
                                image.ImageReport = item;
                                image.Idimages = Guid.NewGuid().ToString();
                                image.AccidentIdAccident = newreport.IdAccident;
                                images.Add(image);
                            }
                            _context.Images.AddRange(images);
                            _context.SaveChanges();

                            var involveds = new List<Involved>();
                            foreach (var item in report.Involveds)
                            {
                                var newinvolved = new Involved();
                                newinvolved.NameInvolved = item.NameInvolved;
                                newinvolved.LastNameInvolved = item.LastNameInvolved;
                                newinvolved.LicenseNumber = item.LicenseNumber;
                                newinvolved.IdInvolved = Guid.NewGuid().ToString();
                                newinvolved.AccidentIdAccident = newreport.IdAccident;
                                if (item.CarInvolved != null)
                                    if (item.CarInvolved.Color != null || item.CarInvolved.Model != null || item.CarInvolved.Plate != null || item.CarInvolved.Brand != null)
                                    {
                                        var newcar = new Carinvolved();
                                        newcar.Color = item.CarInvolved.Color;
                                        newcar.Model = item.CarInvolved.Model;
                                        newcar.Plate = item.CarInvolved.Plate;
                                        newcar.Brand = item.CarInvolved.Brand;
                                        newcar.IdCarInvolved = Guid.NewGuid().ToString();
                                        newinvolved.CarInvolvedIdCarInvolved = newcar.IdCarInvolved;
                                        newinvolved.CarInvolvedIdCarInvolvedNavigation = newcar;
                                    }
                                involveds.Add(newinvolved);
                            }
                            _context.Involveds.AddRange(involveds);
                            _context.SaveChanges();
                            transaction.Commit();
                            result = true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public async Task<ReportDTO> GetReportById(string idReport)
        {
            ReportDTO result = null;
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
                        .Include(c => c.VehicleClientIdVehicleClientNavigation)
                        .Include(c => c.DriverClientIdDriverClientNavigation)
                        .Include(i => i.Images)
                        .Include(e => e.Involveds)
                        .Include(o => o.OpinionAdjusterIdOpinionAdjusterNavigation)
                        .Where(x => x.IdAccident == idReport)
                        .FirstOrDefault();

                    if (report != null)
                    {
                        var itemDTO = new ReportDTO
                        {
                            IdAccident = report.IdAccident,
                            AccidentDate = report.AccidentDate,
                            IdDriverClient = report.DriverClientIdDriverClient,
                            IdVehicleClient = report.VehicleClientIdVehicleClient,
                            Latitude = report.Latitude,
                            Longitude = report.Longitude,
                            Location = report.Location,
                            NameLocation = report.NameLocation,
                            ReportStatus = report.ReportStatus,
                            IdOpinionAdjuster = report.OpinionAdjusterIdOpinionAdjuster
                        };

                        if (report.VehicleClientIdVehicleClientNavigation != null)
                        {
                            itemDTO.VehicleClient = new VehicleclientDTO
                            {
                                IdVehicleClient = report.VehicleClientIdVehicleClientNavigation.IdVehicleClient,
                                Brand = report.VehicleClientIdVehicleClientNavigation.Brand,
                                Model = report.VehicleClientIdVehicleClientNavigation.Model,
                                Color = report.VehicleClientIdVehicleClientNavigation.Color,
                                Plate = report.VehicleClientIdVehicleClientNavigation.Plate,
                                Year = report.VehicleClientIdVehicleClientNavigation.Year,
                                SerialNumber = report.VehicleClientIdVehicleClientNavigation.SerialNumber,
                            };
                        }

                        if (report.DriverClientIdDriverClientNavigation != null)
                        {
                            itemDTO.DriverClient = new DriverclientDTO
                            {
                                IdDriverClient = report.DriverClientIdDriverClientNavigation.IdDriverClient,
                                NameDriver = report.DriverClientIdDriverClientNavigation.NameDriver,
                                LastNameDriver = report.DriverClientIdDriverClientNavigation.LastNameDriver,
                                LicenseNumber = report.DriverClientIdDriverClientNavigation.LicenseNumber,
                                TelephoneNumber = report.DriverClientIdDriverClientNavigation.TelephoneNumber
                            };
                        }

                        itemDTO.Images = report.Images?.Select(item => new ImageDTO { ImageReport = item.ImageReport }).ToList();

                        itemDTO.Involveds = report.Involveds?.Select(item => new InvolvedDTO
                        {
                            LastNameInvolved = item.LastNameInvolved,
                            NameInvolved = item.NameInvolved,
                            LicenseNumber = item.LicenseNumber
                        }).ToList();

                        if (report.OpinionAdjusterIdOpinionAdjusterNavigation != null)
                        {
                            itemDTO.OpinionAdjuster = new OpinionadjusterDTO
                            {
                                CreationDate = (DateTime)report.OpinionAdjusterIdOpinionAdjusterNavigation.CreationDate,
                                Description = report.OpinionAdjusterIdOpinionAdjusterNavigation.Description,
                                IdOpinionAdjuster = report.OpinionAdjusterIdOpinionAdjusterNavigation.IdOpinionAdjuster
                            };
                        }

                        return itemDTO;
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
                .Where(a => a.EmployeeIdEmployee.Equals(idEmployee)).ToList();

                foreach (var item in listTemp)
                {
                    PreviewReportDTO temp = new PreviewReportDTO()
                    {
                        NameClient = item.DriverClientIdDriverClientNavigation.NameDriver + " " + item.DriverClientIdDriverClientNavigation.LastNameDriver,
                        ReportNumber = item.IdAccident,
                        StatusReport = item.ReportStatus,
                        ReportDate = item.AccidentDate.GetValueOrDefault(),
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        LocationName = item.Location
                    };
                    reportsList.Add(temp);
                }
                code = 200;
            }
            catch (Exception)
            {
                code = 500;
            }
            return (code, reportsList);
        }

        public async Task<bool> PutOpinion(NewOpinionadjusterDTO opinion)
        {
            var result = false;
            try
            {
                bool canConnect = await _context.Database.CanConnectAsync();

                if (!canConnect)
                {
                    throw new Exception("No se pudo establecer conexión con la base de datos.");
                }
                else
                {
                    var opinionAdjuster = await _context.Opinionadjusters.Where(i => i.IdOpinionAdjuster == opinion.IdOpinionAdjuster).FirstOrDefaultAsync();
                    if (opinionAdjuster != null)
                    {
                        if (opinionAdjuster.Description != opinion.Description)
                            opinionAdjuster.Description = opinion.Description;
                        await _context.SaveChangesAsync();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<bool> PostOpinion(NewOpinionadjusterDTO opinion)
        {
            var result = false;
            try
            {
                bool canConnect = await _context.Database.CanConnectAsync();

                if (!canConnect)
                {
                    throw new Exception("No se pudo establecer conexión con la base de datos..");
                }
                else
                {
                    var opinionAdjuster = new Opinionadjuster();
                    opinionAdjuster.CreationDate = opinion.CreationDate;
                    opinionAdjuster.Description = opinion.Description;
                    opinionAdjuster.IdOpinionAdjuster = Guid.NewGuid().ToString();

                    var accident = await _context.Accidents.Where(i => i.IdAccident == opinion.IdAccident).FirstOrDefaultAsync();
                    if (accident != null && accident.OpinionAdjusterIdOpinionAdjuster == null)
                    {
                        accident.OpinionAdjusterIdOpinionAdjusterNavigation = opinionAdjuster;
                        _context.Opinionadjusters.Add(opinionAdjuster);
                        accident.ReportStatus = "Dictaminado";

                        await _context.SaveChangesAsync();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;

        }

    }
}

