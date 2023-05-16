using System.ComponentModel.DataAnnotations;

namespace APIpeliculas.Models
{
    public class CinesCreacionModel
    {
        [Required(ErrorMessage ="El campo nombre es obligatorio")]
        public string Nombre { get; set; }

        public double Latitud { get; set; }

        public double Longitud { get; set; }
    }
}
