namespace ServicioTransacciones.API.Modelos
{
    public class Transaccion
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } = "Compra"; // o "Venta"
        public Guid ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        // No se mapea con EF porque ya se calcula en la BD
        public string Detalle { get; set; }
    }
}
