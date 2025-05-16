using System.Net.Http;
using System.Text;
using System.Text.Json;
using ServicioTransacciones.API.DTOs;
using ServicioTransacciones.API.Modelos;
using ServicioTransacciones.API.Repositorios;

namespace ServicioTransacciones.API.Servicios
{
    public class TransaccionServicio : ITransaccionServicio
    {
        private readonly ITransaccionRepositorio _repositorio;
        private readonly IHttpClientFactory _httpClientFactory;

        public TransaccionServicio(ITransaccionRepositorio repositorio, IHttpClientFactory httpClientFactory)
        {
            _repositorio = repositorio;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Transaccion>> ListarAsync()
        {
            return await _repositorio.ObtenerTodasAsync();
        }

        public async Task<Transaccion> ObtenerPorIdAsync(Guid id)
        {
            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task<bool> CrearAsync(TransaccionCrearDTO dto)
        {
            var cliente = _httpClientFactory.CreateClient("Productos");

            // 1. Validar stock si es venta
            if (dto.Tipo == "Venta")
            {
                var respuesta = await cliente.GetAsync($"/api/productos/{dto.ProductoId}");

                if (!respuesta.IsSuccessStatusCode) return false;

                var contenido = await respuesta.Content.ReadAsStringAsync();

                var producto = JsonSerializer.Deserialize<ProductoStockDTO>(contenido, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (producto == null || producto.Stock < dto.Cantidad)
                {
                    return false;
                }
            }

            // 2. Registrar la transacción
            var transaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                Fecha = dto.Fecha,
                Tipo = dto.Tipo,
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario,
                Detalle = dto.Detalle
            };

            await _repositorio.CrearAsync(transaccion);

            // 3. Intentar ajustar el stock
            int ajuste = dto.Tipo == "Venta" ? -dto.Cantidad : dto.Cantidad;

            var body = new AjusteStockDTO { Cantidad = ajuste };

            var contenidoJson = new StringContent(
                JsonSerializer.Serialize(body),
                Encoding.UTF8,
                "application/json"
            );

            var respuestaAjuste = await cliente.PutAsync($"/api/productos/{dto.ProductoId}/stock", contenidoJson);

            if (!respuestaAjuste.IsSuccessStatusCode)
            {
                //Rollback manual -- eliminar transacción si el ajuste falla
                await EliminarTransaccion(transaccion.Id);
                return false;
            }

            return true;
        }

        private async Task EliminarTransaccion(Guid id)
        {
            await _repositorio.EliminarPorIdAsync(id);
        }


    }
}
