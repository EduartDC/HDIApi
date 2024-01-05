using HDIApi.Bussines.Interface;
using HDIApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace HDIApi.Bussines
{
    public class VehicleProvider : IVehicleProvider
    {
        private readonly InsurancedbContext _context;

        public VehicleProvider(InsurancedbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateUniqueVehicleId()
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
                return BitConverter.ToString(hashedBytes).Replace("-", "").Substring(0, 8);
            }
        }

        public async Task<string> AddCustomerVehicle(string brand, string color, string model, string plate, string serialNumber, string year)
        {
            try
            {
                Vehicleclient newVehicle = new Vehicleclient()
                {
                    IdVehicleClient = await GenerateUniqueVehicleId(),
                    Brand = brand,
                    Color = color,
                    Model = model,
                    Plate = plate,
                    SerialNumber = serialNumber,
                    Year = year
                };

                _context.Vehicleclients.Add(newVehicle);
                int change = await _context.SaveChangesAsync();
                if (change == 1)
                    return newVehicle.IdVehicleClient;
                else
                    return "Error al agregar vehiculo";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar vehiculo", ex);
            }
        }
    }
}
