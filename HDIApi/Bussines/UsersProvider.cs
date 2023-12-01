using HDIApi.Bussines.Interface;
using HDIApi.DTOs;
using HDIApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HDIApi.Bussines
{
    public class UsersProvider : IUsersProvider
    {
        private readonly InsurancedbContext _context;

        public UsersProvider(InsurancedbContext context)
        {
            string culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            _context = context;

        }
        public async Task<Employee> LoginEmployee(LoginDTO login)
        {
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                return await _context.Employees.Where(x => x.Username == login.User).FirstOrDefaultAsync();
            }
        }

        public async Task<Driverclient> LoginDriver(LoginDTO login)
        {
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                return await _context.Driverclients.Where(x => x.LicenseNumber == login.User).FirstOrDefaultAsync();
            }
        }
    }
}
