using System.ComponentModel.DataAnnotations;

namespace stevejobstemp.Models
{
    public class Alumno
    {
        [Key]
        public int AlumnoId { get; set; }
        public int? GradoId { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Sexo { get; set; }
        public DateTime? FecNac { get; set; }
        public string? UsuarioId { get; set; }

        public virtual Grado1? Grado { get; set; }

        public string FullName
        {
            get
            {
                return Nombres + " " + Apellidos;
            }
        }
    }

}
