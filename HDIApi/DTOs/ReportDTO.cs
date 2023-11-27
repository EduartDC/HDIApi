using HDIApi.Models;

namespace HDIApi.DTOs
{
    public class ReportDTO
    {
        public string IdAccident { get; set; } = null!;

        public string? Location { get; set; }

        public string? Longitude { get; set; }

        public string? Latitude { get; set; }

        public string? NameLocation { get; set; }

        public string? ReportStatus { get; set; }

        public DateTime? AccidentDate { get; set; }

        public string IdDriverClient { get; set; } = null!;

        public string? IdEmployee { get; set; }

        public string? IdOpinionAdjuster { get; set; }

        public string IdVehicleClient { get; set; } = null!;

        public DriverclientDTO DriverClient { get; set; } = null!;

        public List<ImageDTO> Images { get; set; }

        public List<InvolvedDTO> Involveds { get; } = new List<InvolvedDTO>();

        public OpinionadjusterDTO? OpinionAdjuster { get; set; }

        public VehicleclientDTO VehicleClient { get; set; } = null!;
    }
}
