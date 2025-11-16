using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParcialP3.Domain.Entities;
using ParcialP3.Persistence.Context;
using ParcialP3.Persistence.Interfaces;

namespace ParcialP3.Persistence.Repositories
{
    public class TipoPropiedadRepository : ITipoPropiedadRepository
    {
        private readonly DBContext _dbContext;
        private readonly DbSet<TipoPropiedad> tipos;
        private readonly ILogger<TipoPropiedadRepository> _logger;

        public TipoPropiedadRepository(DBContext dbContext, ILogger<TipoPropiedadRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            tipos = _dbContext.Set<TipoPropiedad>();
        }

        public async Task<List<TipoPropiedad>> GetAll()
        {
            try
            {
                return await tipos
                    .AsNoTracking()
                    .Where(t => t.Activo == true)
                    .Select(t => new TipoPropiedad
                    {
                        Id = t.Id,
                        DESCRIPCION = t.DESCRIPCION,
                        Activo = t.Activo,
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener tipos de propiedad: {Exception}", ex);
                return null;
            }
        }

        public async Task<TipoPropiedad> GetEntityBy(int Id)
        {
            try
            {
                return await tipos.FindAsync(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener tipo de propiedad: {Exception}", ex);
                return null;
            }
        }

        public async Task<bool> Save(TipoPropiedad entity)
        {
            try
            {
                await tipos.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al guardar tipo de propiedad: {Exception}", ex);
                return false;
            }
        }

        public async Task<bool> Update(TipoPropiedad entity)
        {
            try
            {
                tipos.Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al actualizar tipo de propiedad: {Exception}", ex);
                return false;
            }
        }

        public async Task<bool> RemoveById(int id)
        {
            try
            {
                var existente = await tipos.FindAsync(id);
                if (existente is null) return false;

                tipos.Remove(existente);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al eliminar tipo de propiedad: {Exception}", ex);
                return false;
            }
        }
    }
}