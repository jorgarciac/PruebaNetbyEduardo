using Microsoft.EntityFrameworkCore;
using ServicioTransacciones.API.Modelos;

namespace ServicioTransacciones.API.Datos
{
    public class AplicacionDbContext : DbContext
    {

        public AplicacionDbContext(DbContextOptions<AplicacionDbContext> options) : base(options) { }

        public DbSet<Transaccion> Transacciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaccion>()
                .Property(t => t.Tipo)
                .HasMaxLength(10);

            base.OnModelCreating(modelBuilder);
        }
    }
}
