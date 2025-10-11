using ParcialP3.Application.Helpers;
using ParcialP3.Application.Interfaces;
using ParcialP3.Application.ViewModels;

namespace ParcialP3.WEB.Middlewares
{
    public class UserSession : IUserSeccion
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UsersViewModel? userViewModel = _httpContextAccessor.HttpContext?.Session.Get<UsersViewModel>("User");

            if (userViewModel == null)
            {
                return false;
            }

            return true;
        }

        public UsersViewModel? GetUserSeccion()
        {
            UsersViewModel? userViewModel = _httpContextAccessor.HttpContext?.Session.Get<UsersViewModel>("User");

            if (userViewModel == null)
            {
                return null;
            }

            return userViewModel;
        }
    }
}
