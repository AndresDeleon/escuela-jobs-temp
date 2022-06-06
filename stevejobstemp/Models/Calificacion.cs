using System.ComponentModel.DataAnnotations;

namespace stevejobstemp.Models
{
    public class Calificacion
    {
        [Key]
        public int CalificacionId { get; set; }
        public decimal? CalificacionUno { get; set; }
        public decimal? CalificacionDos { get; set; }
        public decimal? CalificacionTres { get; set; }
        public decimal? CalificacionCuatro { get; set; }
        public decimal? CalificacionTotal { get; set; }
    }
}
