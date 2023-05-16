using System.ComponentModel.DataAnnotations;

namespace APIpeliculas.Models
{
    public class CineViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El Nombre es obligatorio")]
        public string Nombre { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
