using System.ComponentModel.DataAnnotations;

namespace HDIApi.DTOs
{
    public class NewOpinionadjusterDTO
    {
        [Required(ErrorMessage = "La fecha es necesaria")]
        public DateTime CreationDate { get; set; }
        [Required(ErrorMessage = "Es necesaria la decision del ajustador")]
        public string Description { get; set; }
        
        public string? IdAccident { get; set; } = null!;
        public string? IdOpinionAdjuster { get; set; } = null!;
    }
}
