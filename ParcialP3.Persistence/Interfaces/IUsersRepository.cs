using ParcialP3.Domain.Entities;
using ParcialP3.Persistence.Base;

namespace ParcialP3.Persistence.Interfaces
{
    public interface IUsersRepository : IBaseRepository<Users>
    {
        Task<Users> LoginAsync(string userName, string password);
    }
}