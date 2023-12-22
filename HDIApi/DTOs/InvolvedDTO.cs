using HDIApi.Models;
using System.ComponentModel.DataAnnotations;

namespace HDIApi.DTOs
{
    public class InvolvedDTO
    {
        [Required]
        public string LastNameInvolved { get; set; }
        [Required]
        public string NameInvolved { get; set; }
        [Required]
        public string LicenseNumber { get; set; }

        public string? IdCarInvolved { get; set; } = null!;

        public CarinvolvedDTO? CarInvolved { get; set; } = null!;
    }
}
