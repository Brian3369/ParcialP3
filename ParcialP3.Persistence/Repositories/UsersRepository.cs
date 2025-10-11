using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParcialP3.Domain.Entities;
using ParcialP3.Persistence.Context;
using ParcialP3.Persistence.Interfaces;

namespace ParcialP3.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DBContext _dbContext;
        private readonly DbSet<Users> users;
        private readonly ILogger<UsersRepository> _logger;

        public UsersRepository(DBContext dbContext, ILogger<UsersRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            users = _dbContext.Set<Users>();
        }

        public async Task<Users> LoginAsync(string userName, string password)
        {
            Users? user = await _dbContext.Set<Users>().FirstOrDefaultAsync
            (u => u.Usuario == userName && u.Password == password);
            return user;
        }

        public async Task<List<Users>> GetAll()
        {
            var datos = new List<Users>();
            try
            {
                datos = await (from Users in users
                               where Users.idEstatus == 1
                               select new Users()
                               {
                                   ID = Users.ID,
                                   Nombre = Users.Nombre,
                                   Usuario = Users.Usuario,
                                   Password = Users.Password,
                                   Email = Users.Email,
                                   Edad = Users.Edad,
                                   idEstatus = Users.idEstatus
                               }).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError("Error:", ex);
                return null;
            }

            return datos;
        }

        public async Task<Users> GetEntityBy(int Id)
        {
            var datos = new Users();
            try
            {
                datos = await this.users.FindAsync(Id);
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
                var entity = await users.FindAsync(id);
                users.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error:", ex);
                return false;
            }

            return true;
        }

        public virtual async Task<bool> Save(Users user)
        {
            try
            {
                users.Add(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error:", ex);
                return false;
            }
            return true;
        }

        public virtual async Task<bool> Update(Users user)
        {
            try
            {
                users.Update(user);
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
