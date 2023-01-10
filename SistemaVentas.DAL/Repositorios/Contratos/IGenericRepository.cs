using System.Linq.Expressions;

namespace SistemaVentas.DAL.Repositorios.Contratos
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        //trabajar de manera azincrona para optener un modelo de culaquier tabla

        Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro);

        //siguiente metodod devulve objeto TModel resivir modelo para poder crear un nuevo categoria etc,
        Task<TModel> Crear(TModel modelo);

        //siguiente metodo vamos resivir modelo para editarlo
        Task<bool> Editar(TModel modelo);

        //metodo para poder eliminar un modelo

        Task<bool> Eliminar(TModel modelo);

        //metodo qu nos va devolver una consulta  <IQuereable> consultas base de datos
        Task<IQueryable<TModel>> Consultar(Expression<Func<TModel, bool>> filtro = null);



    }
}
