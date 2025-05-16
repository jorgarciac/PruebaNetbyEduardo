using ServicioProductos.API.DTOs;
using ServicioProductos.API.Modelos;
using ServicioProductos.API.Repositorios;

namespace ServicioProductos.API.Servicios
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly IProductoRepositorio _repositorio;

        public ProductoServicio(IProductoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<ProductoDTO>> ListarProductosAsync()
        {
            var productos = await _repositorio.ObtenerTodosAsync();
            return productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Categoria = p.Categoria,
                ImagenUrl = p.ImagenUrl,
                Precio = p.Precio,
                Stock = p.Stock
            }).ToList();
        }

        public async Task<ProductoDTO?> ObtenerProductoAsync(Guid id)
        {
            var producto = await _repositorio.ObtenerPorIdAsync(id);
            if (producto == null) return null;

            return new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Categoria = producto.Categoria,
                ImagenUrl = producto.ImagenUrl,
                Precio = producto.Precio,
                Stock = producto.Stock
            };
        }

        public async Task<ProductoDTO> CrearProductoAsync(ProductoCrearDTO dto)
        {
            var producto = new Producto
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Categoria = dto.Categoria,
                ImagenUrl = dto.ImagenUrl,
                Precio = dto.Precio,
                Stock = dto.Stock
            };

            await _repositorio.CrearAsync(producto);

            return new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Categoria = producto.Categoria,
                Precio = producto.Precio,
                Stock = producto.Stock
            };
        }

        public async Task<bool> ActualizarProductoAsync(Guid id, ProductoEditarDTO dto)
        {
            var producto = await _repositorio.ObtenerPorIdAsync(id);
            if (producto == null) return false;

            producto.Nombre = dto.Nombre;
            producto.Descripcion = dto.Descripcion;
            producto.Categoria = dto.Categoria;
            producto.ImagenUrl = dto.ImagenUrl;
            producto.Precio = dto.Precio;
            producto.Stock = dto.Stock;

            await _repositorio.ActualizarAsync(producto);
            return true;
        }

        public async Task<bool> EliminarProductoAsync(Guid id)
        {
            var producto = await _repositorio.ObtenerPorIdAsync(id);
            if (producto == null) return false;

            await _repositorio.EliminarAsync(producto);
            return true;
        }

        public async Task<bool> AjustarStockAsync(Guid id, int cantidad)
        {
            var producto = await _repositorio.ObtenerPorIdAsync(id);
            if (producto == null) return false;

            var nuevoStock = producto.Stock + cantidad;

            if (nuevoStock < 0) return false; // No permitir stock negativo

            producto.Stock = nuevoStock;
            await _repositorio.ActualizarAsync(producto);

            return true;
        }

    }
}
