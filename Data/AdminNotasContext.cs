using API_REST_ADMIN_NOTAS.Models;
using Microsoft.EntityFrameworkCore;
namespace API_REST_ADMIN_NOTAS.Data
{
    public class AdminNotasContext : DbContext
    {
        public AdminNotasContext(DbContextOptions<AdminNotasContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Etiqueta> Etiquetas { get; set; } = null!;
        public DbSet<Nota> Notas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Nota>().ToTable("Notas");
        }
    }
}
