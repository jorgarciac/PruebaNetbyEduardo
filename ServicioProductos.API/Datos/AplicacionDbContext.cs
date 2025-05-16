using Microsoft.EntityFrameworkCore;
using ServicioProductos.API.Modelos;

namespace ServicioProductos.API.Datos
{
    public class AplicacionDbContext : DbContext
    {
        public AplicacionDbContext(DbContextOptions<AplicacionDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
    }
}
