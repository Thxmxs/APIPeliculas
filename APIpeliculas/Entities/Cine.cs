using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace APIpeliculas.Entities
{
    public class Cine
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El Nombre es obligatorio")]
        public string Nombre { get; set; }
        public Point Ubicacion { get; set; }
    }
}
