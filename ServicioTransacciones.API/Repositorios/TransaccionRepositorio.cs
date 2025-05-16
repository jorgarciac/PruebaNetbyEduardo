using Microsoft.EntityFrameworkCore;
using ServicioTransacciones.API.Datos;
using ServicioTransacciones.API.Modelos;

namespace ServicioTransacciones.API.Repositorios
{
    public class TransaccionRepositorio : ITransaccionRepositorio
    {
        private readonly AplicacionDbContext _contexto;

        public TransaccionRepositorio(AplicacionDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Transaccion>> ObtenerTodasAsync() =>
            await _contexto.Transacciones.ToListAsync();

        public async Task<Transaccion> ObtenerPorIdAsync(Guid id) =>
            await _contexto.Transacciones.FindAsync(id);

        public async Task CrearAsync(Transaccion transaccion)
        {
            _contexto.Transacciones.Add(transaccion);
            await _contexto.SaveChangesAsync();
        }

        public async Task EliminarPorIdAsync(Guid id)
        {
            var transaccion = await _contexto.Transacciones.FindAsync(id);
            if (transaccion != null)
            {
                _contexto.Transacciones.Remove(transaccion);
                await _contexto.SaveChangesAsync();
            }
        }

    }
}
