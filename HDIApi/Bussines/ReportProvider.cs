﻿using HDIApi.Bussines.Interface;
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
            try{
                bool canConnect = await _context.Database.CanConnectAsync();

                if(!canConnect){
                    throw new Exception("No se pudo establecer conexión con la base de datos.");
                }
                else{
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try{
                            var newreport = new Accident();
                            newreport.IdAccident = Guid.NewGuid().ToString();
                            newreport.AccidentDate = DateTime.Now;
                            newreport.ReportStatus = "Nuevo";
                            newreport.DriverClientIdDriverClient = report.IdDriverClient;
                            newreport.VehicleClientIdVehicleClient = report.IdVehicleClient;
                            newreport.Latitude = report.Latitude;
                            newreport.Longitude = report.Longitude;
                            newreport.Location = report.Location;

                            _context.Accidents.Add(newreport);
                            _context.SaveChanges();

                            var images = new List<Image>();
                            foreach(var item in report.Images){
                                var image = new Image();
                                image.ImageReport = item;
                                image.Idimages = Guid.NewGuid().ToString();
                                image.AccidentIdAccident = newreport.IdAccident;
                                images.Add(image);
                            }
                            _context.Images.AddRange(images);
                            _context.SaveChanges();

                            var involveds = new List<Involved>();
                            foreach(var item  in report.Involveds){
                                var newinvolved = new Involved();
                                newinvolved.NameInvolved = item.NameInvolved;
                                newinvolved.LastNameInvolved = item.LastNameInvolved;
                                newinvolved.LicenseNumber = item.LicenseNumber;
                                newinvolved.IdInvolved = Guid.NewGuid().ToString();
                                newinvolved.AccidentIdAccident= newreport.IdAccident;
                                if(item.CarInvolved != null)
                                    if(item.CarInvolved.Color != null || item.CarInvolved.Model != null || item.CarInvolved.Plate != null || item.CarInvolved.Brand != null){
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
                        itemDTO.ReportStatus = "Pendiente";
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
                        var involveds = new List<InvolvedDTO>();
                        foreach (var item in report.Involveds)
                        {
                            var involved = new InvolvedDTO();
                            involved.LastNameInvolved = item.LastNameInvolved;
                            involved.NameInvolved = item.NameInvolved;
                            involved.LicenseNumber = item.LicenseNumber;
                            involveds.Add(involved);
                        }
                        itemDTO.Involveds = involveds;
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
                .Where(a => a.EmployeeIdEmployee.Equals(idEmployee)).ToList();

                foreach (var item in listTemp)
                {
                    PreviewReportDTO temp = new PreviewReportDTO()
                    {
                        NameClient = item.DriverClientIdDriverClientNavigation.NameDriver,
                        ReportNumber = item.IdAccident,
                        StatusReport = item.ReportStatus,
                        ReportDate = item.AccidentDate.GetValueOrDefault(),
                        Latitude = item.Latitude,
                        Longitude = item.Longitude
                    };
                    reportsList.Add(temp);
                }
                code = 200;
            } catch (Exception)
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
                    if(opinionAdjuster != null){
                        if(opinionAdjuster.Description != opinion.Description)
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
                    throw new Exception("No se pudo establecer conexión con la base de datos.");
                }
                else
                {
                    var opinionAdjuster = new Opinionadjuster();
                    opinionAdjuster.CreationDate = opinion.CreationDate;
                    opinionAdjuster.Description = opinion.Description;
                    opinionAdjuster.IdOpinionAdjuster = Guid.NewGuid().ToString();

                    var accident = await _context.Accidents.Where(i => i.IdAccident == opinion.IdAccident).FirstOrDefaultAsync();
                    if(accident != null && accident.OpinionAdjusterIdOpinionAdjuster == null){
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

