using HDIApi.Models;

namespace HDIApi.DTOs
{
    public class VehicleclientDTO
    {
        public string IdVehicleClient { get; set; } = null!;

        public string? Brand { get; set; }

        public string? Color { get; set; }

        public string? Model { get; set; }

        public string? Plate { get; set; }

        public string? SerialNumber { get; set; }

        public string? Year { get; set; }

        public string IdInsurancePolicy { get; set; } = null!;
    }
}
