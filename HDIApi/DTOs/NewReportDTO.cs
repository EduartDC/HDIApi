using HDIApi.Models;
using System.ComponentModel.DataAnnotations;

namespace HDIApi.DTOs
{
    public class NewReportDTO
    {
        [Required]
        public string? Location { get; set; }
        [Required]
        public string Latitude { get; set; } = null!;
        [Required]
        public string Longitude { get; set; } = null!;
        [Required]
        public string NameLocation { get; set; } = null!;
        [Required]
        public string IdVehicleClient { get; set; } = null!;
        [Required]
        public string IdDriverClient { get; set; } = null!;
        [Required]
        public  List<ImageDTO> Images { get; } = new List<ImageDTO>();
        [Required]
        public  List<InvolvedDTO> Involveds { get; } = new List<InvolvedDTO>();
    }
}
