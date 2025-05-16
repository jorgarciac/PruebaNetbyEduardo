using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioProductos.API.Datos;
using ServicioProductos.API.DTOs;
using ServicioProductos.API.Modelos;
using ServicioProductos.API.Servicios;

namespace ServicioProductos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {

        private readonly IProductoServicio _productoServicio;

        public ProductosController(IProductoServicio productoServicio)
        {
            _productoServicio = productoServicio;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> ObtenerTodos()
        {
            var productos = await _productoServicio.ListarProductosAsync();
            return Ok(productos);
        }

        // GET: api/Productos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> ObtenerPorId(Guid id)
        {
            var producto = await _productoServicio.ObtenerProductoAsync(id);
            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> Crear([FromBody] ProductoCrearDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productoCreado = await _productoServicio.CrearProductoAsync(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = productoCreado.Id }, productoCreado);
        }

        // PUT: api/Productos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ProductoEditarDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var actualizado = await _productoServicio.ActualizarProductoAsync(id, dto);
            if (!actualizado)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Productos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var eliminado = await _productoServicio.EliminarProductoAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }


        [HttpPut("{id}/stock")]
        public async Task<IActionResult> AjustarStock(Guid id, [FromBody] AjusteStockDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var resultado = await _productoServicio.AjustarStockAsync(id, dto.Cantidad);
            if (!resultado) return BadRequest("No se pudo ajustar el stock. Verifique que el producto exista o que la cantidad no cause stock negativo.");

            return Ok("Stock ajustado correctamente.");
        }

    }
}
