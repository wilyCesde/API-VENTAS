using Microsoft.EntityFrameworkCore;
using SistemaVentas.DAL.DBContext;
using SistemaVentas.DAL.Repositorios.Contratos;
using System.Linq.Expressions;

namespace SistemaVentas.DAL.Repositorios
{
    public class GenericRepository<TModelo> : IGenericRepository<TModelo> where TModelo : class
    {
        private readonly DbventaContext _dbcontext;

        public GenericRepository(DbventaContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<TModelo> Obtener(Expression<Func<TModelo, bool>> filtro)
        {
            try
            {
                return await _dbcontext.Set<TModelo>().FirstOrDefaultAsync(filtro);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el modelo en la base de datos", ex);
            }
        }
        public async Task<TModelo> Crear(TModelo modelo)
        {
            try
            {
                _dbcontext.Set<TModelo>().Add(modelo);
                await _dbcontext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el modelo en la base de datos", ex);
            }
        }

        public async Task<bool> Editar(TModelo modelo)
        {
            try
            {
                _dbcontext.Set<TModelo>().Update(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el modelo en la base de datos", ex);
            }
        }

        public async Task<bool> Eliminar(TModelo modelo)
        {
            _dbcontext.Set<TModelo>().Remove(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<IQueryable<TModelo>> Consultar(Expression<Func<TModelo, bool>> filtro = null)
        {
            try
            {
                IQueryable<TModelo> queryModelo = filtro == null ?
                 _dbcontext.Set<TModelo>() : _dbcontext.Set<TModelo>().Where(filtro);
                return await Task.FromResult(queryModelo);


            }
            catch (Exception ex)
            {
                // Aquí puedes manejar el error de alguna manera, por ejemplo lanzando una excepción personalizada
                throw new Exception("Error al consultar en la base de datos", ex);
            }
        }

    }
}
