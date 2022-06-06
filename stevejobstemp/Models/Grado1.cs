using System.ComponentModel.DataAnnotations;

namespace stevejobstemp.Models
{
    public class Grado1
    {
        [Key]
        public int GradoId { get; set; }
        public string? Grado { get; set; }
    }
}
