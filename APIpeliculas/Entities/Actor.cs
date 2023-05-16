using System.ComponentModel.DataAnnotations;

namespace APIpeliculas.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo Nombre es requerido")]
        [StringLength(maximumLength:200)]
        public string Nombre { get; set; }

        public string Biografia { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Foto { get; set; }
    }
}
