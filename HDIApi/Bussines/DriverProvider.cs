using Microsoft.AspNetCore.Http.Features;
using HDIApi.DTOs;
using HDIApi.Models;
using System.Globalization;
using HDIApi.Utility;
using HDIApi.Bussines.Interface;

namespace HDIApi.Providers
{
    public class DriverProvider : IDriverProvider
    {
        private InsurancedbContext connectionModel;

        public DriverProvider(InsurancedbContext _connectionModel)
        {
            string Culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Culture);
            connectionModel = _connectionModel;
        }

        public Task<int> SetNewDriver(DriverclientDTO newDriverClient)
        {
            int code = 0;
            var driverTemp = connectionModel.Driverclients.Where(a => a.LicenseNumber.Equals(newDriverClient.LicenseNumber)).FirstOrDefault();
            if (driverTemp == null)
            {
                Driverclient driverModel = new Driverclient()
                {
                    IdDriverClient = Utility.GenerateRandomID.GenerateID(),
                    NameDriver = newDriverClient.NameDriver,
                    LastNameDriver = newDriverClient.LastNameDriver,
                    Age = newDriverClient.Age,
                    TelephoneNumber = newDriverClient.TelephoneNumber,
                    LicenseNumber = newDriverClient.LicenseNumber,
                    Password = newDriverClient.Password
                };
                connectionModel.Driverclients.Add(driverModel);
                int change = connectionModel.SaveChanges();
                if (change == 1)
                    code = 200;
                else
                    code = 500;
            }
            else
                code = 409;//Conflito - ya hay alguien con la licencia registado
            return Task.FromResult(code);
        }
    }
}
