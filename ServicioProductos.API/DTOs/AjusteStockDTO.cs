using System.ComponentModel.DataAnnotations;

namespace ServicioProductos.API.DTOs
{
    public class AjusteStockDTO
    {
        [Required]
        public int Cantidad { get; set; } // Puede ser positiva (compra) o negativa (venta)
    }
}
