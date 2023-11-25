using System.ComponentModel.DataAnnotations;

namespace HDIApi.DTOs
{
    public class NewOpinionadjusterDTO
    {
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string IdAccident { get; set; } = null!;
    }
}
