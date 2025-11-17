using Microsoft.EntityFrameworkCore;
using ParcialP3.Domain.Entities;
using ParcialP3.Persistence.Context;
using ParcialP3.Persistence.Interfaces;

namespace ParcialP3.Persistence.Repositories
{
    public class CiudadesRepository : ICiudadesRepository
    {
        private readonly DBContext _context;

        public CiudadesRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<List<Ciudades>> GetAll()
        {
            return await _context.Set<Ciudades>().ToListAsync();
        }

        public async Task<Ciudades> GetEntityBy(int Id)
        {
            return await _context.Set<Ciudades>().FirstAsync(e => e.Id == Id);
        }

        public async Task<bool> Save(Ciudades entity)
        {
            await _context.Set<Ciudades>().AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Ciudades entity)
        {
            _context.Set<Ciudades>().Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveById(int id)
        {
            var entity = await GetEntityBy(id);
            _context.Set<Ciudades>().Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}