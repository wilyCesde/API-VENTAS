using SistemaVentas.Model;

namespace SistemaVentas.DAL.Repositorios.Contratos
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {
        Task<Venta> Registrar(Venta modelo);
    }
}
