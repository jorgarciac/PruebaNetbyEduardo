using ServicioTransacciones.API.Modelos;

namespace ServicioTransacciones.API.Repositorios
{
    public interface ITransaccionRepositorio
    {
        Task<List<Transaccion>> ObtenerTodasAsync();
        Task<Transaccion> ObtenerPorIdAsync(Guid id);
        Task CrearAsync(Transaccion transaccion);

        Task EliminarPorIdAsync(Guid id);
    }
}
