using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stevejobstemp.Models
{
    public class NonimaDocente
    {
        [Key]
        public int NominaDocenteId { get; set; }
        public int? DocenteId { get; set; }
        public int? MateriaId { get; set; }
        public int? GradoId { get; set; }

        [NotMapped]
        public string? Nombres { get; set; }
        [NotMapped]
        public string? Apellidos { get; set; }
        [NotMapped]
        public string? Materia { get; set; }
        [NotMapped]
        public string? Grado { get; set; }

        public string FullName
        {
            get
            {
                return Nombres + " " + Apellidos;
            }
        }
    }
}
