using Microsoft.EntityFrameworkCore;
using stevejobstemp.Models;

namespace stevejobstemp.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Docente> Docente { get; set; }

        public DbSet<Alumno> Alumno { get; set; }

        public DbSet<Grado1> Grado { get; set; }

        public DbSet<Calificacion> Calificacion { get; set; }
        public DbSet<Materium> Materia { get; set; }
        public DbSet<NonimaDocente> NonimaDocente { get; set; }
        public DbSet<Notum> Nota { get; set; }
        public DbSet<NonimaAlumno> NonimaAlumno { get; set; }
    }
}
