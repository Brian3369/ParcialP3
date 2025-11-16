using ParcialP3.Application.ViewModels.Users;

namespace ParcialP3.Application.Interfaces
{
    public interface IUserSeccion
    {
        UsersViewModel? GetUserSeccion();
        bool HasUser();
    }
}
