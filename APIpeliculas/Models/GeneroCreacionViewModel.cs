using APIpeliculas.Validations;
using System.ComponentModel.DataAnnotations;

namespace APIpeliculas.Models
{
    public class GeneroCreacionViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength: 50, ErrorMessage = "El maximo es 50 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
