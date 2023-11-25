using HDIApi.Models;
using System.ComponentModel.DataAnnotations;

namespace HDIApi.DTOs
{
    public class PolicyDTO
    {
        public string IdInsurancePolicy { get; set; } = null!;
        public DateTime? StartTerm { get; set; }
        public DateTime? EndTerm { get; set; }
        public int? TermAmount { get; set; }
        public double? Price { get; set; }
        public string? PolicyType { get; set; }
        public string? IdDriverClient { get; set; } = null!;
        public string? IdVehicleClient { get; set; } = null!;
        public VehicleclientDTO? VehicleClient { get; set; } = null!;

    }
}
