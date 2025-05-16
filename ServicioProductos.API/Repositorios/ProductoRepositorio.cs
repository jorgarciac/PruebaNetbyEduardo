using Microsoft.EntityFrameworkCore;
using ServicioProductos.API.Datos;
using ServicioProductos.API.Modelos;

namespace ServicioProductos.API.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly AplicacionDbContext _contexto;

        public ProductoRepositorio(AplicacionDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Producto>> ObtenerTodosAsync() =>
            await _contexto.Productos.ToListAsync();

        public async Task<Producto> ObtenerPorIdAsync(Guid id) =>
            await _contexto.Productos.FindAsync(id);

        public async Task CrearAsync(Producto producto)
        {
            _contexto.Productos.Add(producto);
            await _contexto.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Producto producto)
        {
            _contexto.Productos.Update(producto);
            await _contexto.SaveChangesAsync();
        }

        public async Task EliminarAsync(Producto producto)
        {
            _contexto.Productos.Remove(producto);
            await _contexto.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsync(Guid id) =>
            await _contexto.Productos.AnyAsync(p => p.Id == id);
    }
}

