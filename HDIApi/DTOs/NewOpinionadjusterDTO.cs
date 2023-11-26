using System.ComponentModel.DataAnnotations;

namespace HDIApi.DTOs
{
    public class NewOpinionadjusterDTO
    {
        [Required(ErrorMessage = "La fecha es necesaria")]
        public DateTime CreationDate { get; set; }
        [Required(ErrorMessage = "Es necesaria la decision del ajustador")]
        public string Description { get; set; }
        [Required (ErrorMessage = "Es necesario el id del reporte")]
        public string IdAccident { get; set; } = null!;
    }
}
