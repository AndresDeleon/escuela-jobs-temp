using System.ComponentModel.DataAnnotations;

namespace stevejobstemp.Models
{
    public class Materium
    {
        [Key]
        public int MateriaId { get; set; }
        public string? Materia { get; set; }
    }
}
