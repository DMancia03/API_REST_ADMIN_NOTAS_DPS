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
            modelBuilder.Entity<Etiqueta>()
                .HasOne(e => e.Usuario)
                .WithMany(u => u.Etiquetas)
                .HasForeignKey(e => e.IdUsuario)
                .HasConstraintName("FK_ETIQUETA_USUARIO")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Nota>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Notas)
                .HasForeignKey(a => a.IdUsuario)
                .HasConstraintName("FK_NOTA_USUARIO")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Nota>()
                .HasOne(a => a.Etiqueta)
                .WithMany(e => e.Notas)
                .HasForeignKey(a => a.IdEtiqueta)
                .HasConstraintName("FK_NOTA_ETIQUETA")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
