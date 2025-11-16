using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParcialP3.Domain.Entities;
using ParcialP3.Persistence.Context;
using ParcialP3.Persistence.Interfaces;

namespace ParcialP3.Persistence.Repositories
{
    public class InmuebleRepository : IInmuebleRepository
    {
        private readonly DBContext _dbContext;
        private readonly DbSet<Inmuebles> inmuebles;
        private readonly ILogger<InmuebleRepository> _logger;

        public InmuebleRepository(DBContext dbContext, ILogger<InmuebleRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            inmuebles = _dbContext.Set<Inmuebles>();
        }

        public async Task<List<Inmuebles>> GetAll()
        {
            var datos = new List<Inmuebles>();
            try
            {
                datos = await (from i in inmuebles
                               where i.Activo == true
                               select new Inmuebles
                               {
                                   Id = i.Id,
                                   NombreInmueble = i.NombreInmueble,
                                   Direccion = i.Direccion,
                                   TipoPropiedadId = i.TipoPropiedadId,
                                   CondicionId = i.CondicionId,
                                   CiudadId = i.CiudadId,
                                   Inmuebleimagenid = i.Inmuebleimagenid,
                                   Precio = i.Precio,
                                   Habitaciones = i.Habitaciones,
                                   Baños = i.Baños,
                                   Descripcion = i.Descripcion,
                                   TipoNegocio = i.TipoNegocio,
                                   Activo = i.Activo
                               }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Exception}", ex);
                return null;
            }
            return datos;
        }

        public async Task<Inmuebles> GetEntityBy(int Id)
        {
            var datos = new Inmuebles();
            try
            {
                datos = await this.inmuebles.FindAsync(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error:", ex);
                return null;
            }
            return datos;
        }

        public async virtual Task<bool> RemoveById(int id)
        {
            try
            {
                var entity = await inmuebles.FindAsync(id);
                inmuebles.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error:", ex);
                return false;
            }

            return true;
        }

        public virtual async Task<bool> Save(Inmuebles entity)
        {
            try
            {
                inmuebles.Add(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Exception}", ex);
                return false;
            }
            return true;
        }

        public virtual async Task<bool> Update(Inmuebles entity)
        {
            try
            {
                inmuebles.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error:", ex);
                return false;
            }
            return true;
        }
    }
}
