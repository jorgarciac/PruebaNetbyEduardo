namespace ServicioTransacciones.API.DTOs
{
    public class ProductoStockDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
    }
}
