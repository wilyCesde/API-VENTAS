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
        public VentaRepository(DbventaContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();

            using (var transaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto producto_encontrado = _dbcontext.Productos.Where
                            (p => p.IdProducto == dv.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _dbcontext.Productos.Update(producto_encontrado);

                    }

                    await _dbcontext.SaveChangesAsync();

                    NumeroDocumento correlativo = _dbcontext.NumeroDocumentos.First();
                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbcontext.NumeroDocumentos.Update(correlativo);
                    await _dbcontext.SaveChangesAsync();

                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();

                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);

                    //ACTUALIZAR NUEMRO DE DOCUMENTO

                    modelo.NumeroDocumento = numeroVenta;

                    //Acceder base de dtos 

                    await _dbcontext.Venta.AddAsync(modelo);

                    //guardar de anera asyncrona 

                    await _dbcontext.SaveChangesAsync();

                    //llamar vena generada pasmos el modelo

                    ventaGenerada = modelo;
                    transaction.Commit();
                    return ventaGenerada;



                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al registrar la venta: " + ex.Message);





                }
            }
        }

    }
}