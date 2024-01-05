using HDIApi.Models;
using System.ComponentModel.DataAnnotations;

namespace HDIApi.DTOs
{
    public class NewReportDTO
    {
        [Required]
        public string Location { get; set; }
        [Required]
        public string Longitude { get; set; }
        [Required]
        public string Latitude { get; set; }
        public string ReportStatus { get; set; }
        [Required]
        public DateTime AccidentDate { get; set; }
        [Required]
        public string IdDriverClient { get; set; }
        [Required]
        public string IdVehicleClient { get; set; }
        [Required]
        public List<string> Images { get; set; }
        [Required]
        public List<InvolvedDTO> Involveds { get; set; }
    }
}
