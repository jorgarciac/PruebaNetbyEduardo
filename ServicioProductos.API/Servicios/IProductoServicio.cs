using ServicioProductos.API.DTOs;
using ServicioProductos.API.Modelos;

namespace ServicioProductos.API.Servicios
{
    public interface IProductoServicio
    {
        Task<List<ProductoDTO>> ListarProductosAsync();
        Task<ProductoDTO?> ObtenerProductoAsync(Guid id);
        Task<ProductoDTO> CrearProductoAsync(ProductoCrearDTO dto);
        Task<bool> ActualizarProductoAsync(Guid id, ProductoEditarDTO dto);
        Task<bool> EliminarProductoAsync(Guid id);
        Task<bool> AjustarStockAsync(Guid id, int cantidad);
    }
}
