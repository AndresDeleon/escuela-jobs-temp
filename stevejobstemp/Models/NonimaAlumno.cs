using System.ComponentModel.DataAnnotations;

namespace stevejobstemp.Models
{
    public class NonimaAlumno
    {
        [Key]
        public int NominaAlumnoId { get; set; }
        public int? AlumnoId { get; set; }
        public int? GradoId { get; set; }

        public virtual Alumno? Alumno { get; set; }
        public virtual Grado1? Grado { get; set; }
    }
}
