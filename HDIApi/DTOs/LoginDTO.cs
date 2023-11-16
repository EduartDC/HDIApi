using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HDIApi.DTOs
{
    public class LoginDTO
    {
        
            [Required(ErrorMessage = "El usuario no puede estar vacío")]
            [StringLength(100, ErrorMessage = "El nombre no puede exceder las 100 posiciones")]
            [DataType(DataType.EmailAddress)]
            [DisplayName("Nombre del Usuario")]
            public string User { get; set; }

            [Required(ErrorMessage = "La contraseña no puede estar vacía")]
            [StringLength(200, ErrorMessage = "La contraseña no puede exceder las 200 posiciones")]
            [DisplayName("Contraseña del Usuario")]
            public string Password { get; set; }
        
    }
}
