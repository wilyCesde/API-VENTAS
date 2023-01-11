using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVentas.DAL.DBContext;

namespace SistemaVentas.IOC
{
    public static class Dependencia
    {
        //creamos metodo resivir un servicio de colecciones   llamado metodo de extension

        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbventaContext>(Options =>
            {
                Options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });
        }
    }
}
