﻿namespace ServicioProductos.API.DTOs
{
    public class ProductoDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public string ImagenUrl { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
