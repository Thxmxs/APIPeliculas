using System.ComponentModel.DataAnnotations;

namespace APIpeliculas.Models
{
    public class ActorCreacionModel
    {
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [StringLength(maximumLength: 200)]
        public string Nombre { get; set; }

        public string Biografia { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public IFormFile Foto { get; set; }
    }
}
