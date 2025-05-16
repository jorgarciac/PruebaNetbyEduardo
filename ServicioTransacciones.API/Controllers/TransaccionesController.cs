using Microsoft.AspNetCore.Mvc;
using ServicioTransacciones.API.DTOs;
using ServicioTransacciones.API.Servicios;

namespace ServicioTransacciones.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionesController : ControllerBase
    {
        private readonly ITransaccionServicio _servicio;

        public TransaccionesController(ITransaccionServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _servicio.ListarAsync();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(Guid id)
        {
            var item = await _servicio.ObtenerPorIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] TransaccionCrearDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ok = await _servicio.CrearAsync(dto);
            return ok
                ? Ok(new { mensaje = "Transacción registrada" })
                : BadRequest(new { error = "Error al registrar" });
        }
    }
}
