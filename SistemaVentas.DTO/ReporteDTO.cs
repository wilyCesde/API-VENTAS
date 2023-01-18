namespace SistemaVentas.DTO
{
    public class ReporteDTO
    {
        public string? NumeroDocumento { get; set; }
        public string? TipoPago { get; set; }
        public string? FechaRegistro { get; set; }
        public string? TotalVenta { get; set; }
        public string? Producto { get; set; }
        public int? Cantidad { get; set; }
        public string? Precio { get; set; }
    }
}
