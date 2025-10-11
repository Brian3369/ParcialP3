using ParcialP3.Domain.Entities;

namespace ParcialP3.Persistence.Interfaces
{
    public interface IUsersRepository
    {
        Task<Users> LoginAsync(string userName, string password);
        Task<List<Users>> GetAll();
        Task<Users> GetEntityBy(int Id);
        Task<bool> RemoveById(int id);
        Task<bool> Save(Users user);
        Task<bool> Update(Users user);
    }
}