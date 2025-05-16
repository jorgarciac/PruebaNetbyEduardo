using ServicioTransacciones.API.DTOs;
using ServicioTransacciones.API.Modelos;

namespace ServicioTransacciones.API.Servicios
{
    public interface ITransaccionServicio
    {
        Task<List<Transaccion>> ListarAsync();
        Task<Transaccion> ObtenerPorIdAsync(Guid id);
        Task<bool> CrearAsync(TransaccionCrearDTO dto);
    }
}
