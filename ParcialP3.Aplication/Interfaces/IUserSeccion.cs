using ParcialP3.Application.ViewModels;

namespace ParcialP3.Application.Interfaces
{
    public interface IUserSeccion
    {
        UsersViewModel? GetUserSeccion();
        bool HasUser();
    }
}
