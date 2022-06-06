using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stevejobstemp.Models
{
    public class Docente
    {
        [Key]
        public int DocenteId { get; set; }
        public int? GradoId { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Sexo { get; set; }
        public DateTime? FecNac { get; set; }
        public string? UsuarioId { get; set; }
        public int? Role { get; set; }

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
