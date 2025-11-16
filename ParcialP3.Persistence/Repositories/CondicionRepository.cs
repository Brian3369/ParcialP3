using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParcialP3.Domain.Entities;
using ParcialP3.Persistence.Context;
using ParcialP3.Persistence.Interfaces;

namespace ParcialP3.Persistence.Repositories
{
    public class CondicionRepository : ICondicionRepository
    {
        private readonly DBContext _dbContext;
        private readonly DbSet<Condicion> condiciones;
        private readonly ILogger<CondicionRepository> _logger;

        public CondicionRepository(DBContext dbContext, ILogger<CondicionRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            condiciones = _dbContext.Set<Condicion>();
        }

        public async Task<List<Condicion>> GetAll()
        {
            try
            {
                // Solo activas; AsNoTracking para mejor rendimiento en lectura
                return await condiciones
                    .AsNoTracking()
                    .Where(c => c.Activo)
                    .Select(c => new Condicion
                    {
                        Id = c.Id,
                        DESCRIPCION = c.DESCRIPCION,
                        Activo = c.Activo
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener condiciones: {Exception}", ex);
                return null;
            }
        }

        public async Task<Condicion> GetEntityBy(int Id)
        {
            try
            {
                return await condiciones.FindAsync(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener condición por Id: {Exception}", ex);
                return null;
            }
        }

        public async Task<bool> Save(Condicion entity)
        {
            try
            {
                await condiciones.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al guardar condición: {Exception}", ex);
                return false;
            }
        }

        public async Task<bool> Update(Condicion entity)
        {
            try
            {
                condiciones.Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al actualizar condición: {Exception}", ex);
                return false;
            }
        }

        public async Task<bool> RemoveById(int id)
        {
            try
            {
                var existente = await condiciones.FindAsync(id);
                if (existente is null) return false;

                condiciones.Remove(existente);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al eliminar condición: {Exception}", ex);
                return false;
            }
        }
    }
}