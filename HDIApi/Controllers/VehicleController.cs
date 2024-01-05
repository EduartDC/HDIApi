using HDIApi.Bussines.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HDIApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleProvider _vehicleProvider;

        public VehicleController(IVehicleProvider vehicleProvider, ILogger<VehicleController> logger)
        {
            _logger = logger;
            _vehicleProvider = vehicleProvider;
        }

        [HttpPost("AddCustomerVehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<String> AddCustomerVehicle(string brand, string color, string model, string plate, string serialNumber, string year)
        {
            string result;
            try
            {
                string success = await _vehicleProvider.AddCustomerVehicle(brand, color, model, plate, serialNumber, year);

                if (!success.IsNullOrEmpty())
                {
                    result = success;
                }
                else
                {
                    result = "Error al agregar vehiculo";
                }
            }
            catch (Exception ex)
            {
                result = "Error al agregar vehiculo";
            }
            return result;
        }
    }
}
