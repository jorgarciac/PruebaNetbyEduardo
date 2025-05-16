using ServicioProductos.API.Modelos;

namespace ServicioProductos.API.Repositorios
{
    public interface IProductoRepositorio
    {
        Task<List<Producto>> ObtenerTodosAsync();
        Task<Producto> ObtenerPorIdAsync(Guid id);
        Task CrearAsync(Producto producto);
        Task ActualizarAsync(Producto producto);
        Task EliminarAsync(Producto producto);
        Task<bool> ExisteAsync(Guid id);
    }
}
