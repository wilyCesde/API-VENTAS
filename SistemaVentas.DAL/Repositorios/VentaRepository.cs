using SistemaVentas.DAL.DBContext;
using SistemaVentas.DAL.Repositorios.Contratos;
using SistemaVentas.Model;

namespace SistemaVentas.DAL.Repositorios
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        //crear la variable 

        private readonly DbventaContext _dbcontext;


        //generamos el contructor
        public VentaRepository(DbventaContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            try
            {
                // Agregamos el modelo de venta al contexto de la base de datos
                _dbcontext.Add(modelo);
                // Guardamos los cambios en la base de datos
                await _dbcontext.SaveChangesAsync();
                // Retornamos el modelo de venta registrado
                return modelo;
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar el error de alguna manera, por ejemplo lanzando una excepción personalizada
                throw new Exception("Error al registrar la venta en la base de datos", ex);
            }
        }

    }
}
