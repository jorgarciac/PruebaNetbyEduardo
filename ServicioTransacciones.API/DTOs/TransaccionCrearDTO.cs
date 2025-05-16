using System.ComponentModel.DataAnnotations;

namespace ServicioTransacciones.API.DTOs
{
    public class TransaccionCrearDTO
    {
        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [RegularExpression("Compra|Venta", ErrorMessage = "El tipo debe ser 'Compra' o 'Venta'.")]
        public string Tipo { get; set; }

        [Required]
        public Guid ProductoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
        public int Cantidad { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor que 0.")]
        public decimal PrecioUnitario { get; set; }

        [StringLength(250)]
        public string Detalle { get; set; }
    }
}
