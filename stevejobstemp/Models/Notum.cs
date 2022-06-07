using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stevejobstemp.Models
{
    public class Notum
    {
        [Key]
        public int NotaId { get; set; }
        public int? AlumnoId { get; set; }
        public int? MateriaId { get; set; }
        public decimal? CalificacionUno { get; set; }
        public decimal? CalificacionDos { get; set; }
        public decimal? CalificacionTres { get; set; }
        public decimal? CalificacionCuatro { get; set; }
        [DisplayFormat(DataFormatString = "{0:N1}", ApplyFormatInEditMode = true)]
        public decimal? CalificacionTotal { get; set; }


        public virtual Alumno? Alumno { get; set; }
        public virtual Materium? Materia { get; set; }
    }
}
