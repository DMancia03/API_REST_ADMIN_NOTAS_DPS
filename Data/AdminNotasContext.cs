using Microsoft.EntityFrameworkCore;
namespace API_REST_ADMIN_NOTAS.Data
{
    public class AdminNotasContext : DbContext
    {
        public AdminNotasContext(DbContextOptions<AdminNotasContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Nota>().ToTable("Notas");
        }
    }
}
