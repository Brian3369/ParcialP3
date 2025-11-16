//using InvestmentApp.Core.Application.Dtos.User;
//using InvestmentApp.Core.Application.Helpers;
//using InvestmentApp.Core.Application.Interfaces;
//using InvestmentApp.Core.Application.ViewModels.User;
//using InvestmentApp.Core.Domain.Common.Enums;
//using ItlaInvestmentApp.Core.Application.Interfaces;
//using ItlaInvestmentApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using ParcialP3.Application.Interfaces;
using ParcialP3.Domain.Enums;
using ParcialP3.Persistence.Interfaces;
using ParcialP3.Application.Helpers;
using ParcialP3.Domain.Entities;
using ParcialP3.Application.ViewModels.Users;

namespace ItlaInvestmentApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsersRepository _userRepository;
        private readonly IUserSeccion _userSession;
        public LoginController(IUsersRepository userRepository, IUserSeccion userSession)
        {
            _userRepository = userRepository;
            _userSession = userSession;
        }

        public IActionResult Index()
        {
            if (_userSession.HasUser())
            {
                UsersViewModel user = _userSession.GetUserSeccion();

                if (user != null) 
                {
                    switch (user.idEstatus)
                    {
                        case (int)UserEnums.active:
                            return RedirectToRoute(new { controller = "Home", action = "Index" });
                        default:
                            return RedirectToRoute(new { controller = "Login", action = "Index" });
                    }
                }
            }
            return View(new LoginViewModel() { Password = "", Usuario = "" });
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (_userSession.HasUser())
            {
                UsersViewModel uservm = _userSession.GetUserSeccion();

                if (uservm != null)
                {
                    switch (uservm.idEstatus)
                    {
                        case (int)UserEnums.active:
                            return RedirectToRoute(new { controller = "Home", action = "Index" });
                        default:
                            return RedirectToRoute(new { controller = "Login", action = "Index" });
                    }
                }
            }
            if (!ModelState.IsValid)
            {
                vm.Password = "";
                return View(vm);
            }

            var user = await _userRepository.LoginAsync(vm.Usuario, vm.Password);

            if (user == null)
            {
                ModelState.AddModelError("userValidation", "Usuario o contraseña incorrectos");
                vm.Password = "";
                return View(vm);
            }

            UsersViewModel users = new()
            {
                ID = user.ID,
                Nombre = user.Nombre,
                Usuario = user.Usuario,
                Email = user.Email,
                Edad = user.Edad,
                idEstatus = user.idEstatus
            };

            HttpContext.Session.Set<UsersViewModel>("User", users);

            if (user != null)
            {
                if (user.idEstatus == (int)UserEnums.active)
                {
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
            }
            else
            {
                ModelState.AddModelError("userValidation", "Usuario o contraseña incorrectos");
            }

            vm.Password = "";
            return View(vm); 
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        public IActionResult Register()
        {
            if (_userSession.HasUser())
            {
                UsersViewModel user = _userSession.GetUserSeccion();

                if (user != null)
                {
                    switch (user.idEstatus)
                    {
                        case (int)UserEnums.active:
                            return RedirectToRoute(new { controller = "Home", action = "Index" });
                        default:
                            return RedirectToRoute(new { controller = "Login", action = "Index" });
                    }
                }
            }
            return View(new RegisterUsersViewModel()
            {
                Nombre = "",
                Email = "",
                Usuario = "",
                Password = "",
                Edad = 0,
                idEstatus = (int)UserEnums.active
            });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUsersViewModel vm)
        {
            if (_userSession.HasUser())
            {
                UsersViewModel user = _userSession.GetUserSeccion();

                if (user != null)
                {
                    switch (user.idEstatus)
                    {
                        case (int)UserEnums.active:
                            return RedirectToRoute(new { controller = "Home", action = "Index" });
                        default:
                            return RedirectToRoute(new { controller = "Login", action = "Index" });
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // Crear entidad User a partir del ViewModel
            Users newUser = new()
            {
                Nombre = vm.Nombre,
                Usuario = vm.Usuario,
                Password = vm.Password, // En producción, deberías hashear la contraseña
                Email = vm.Email,
                Edad = vm.Edad,
                idEstatus = vm.idEstatus
            };

            // Guardar el usuario usando el repositorio
            bool result = await _userRepository.Save(newUser);

            if (result)
            {
                // Registro exitoso, redirigir al login
                TempData["SuccessMessage"] = "Usuario registrado exitosamente";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            else
            {
                // Error al guardar
                ModelState.AddModelError("", "Error al registrar el usuario. Intente nuevamente.");
                return View(vm);
            }
        }

        public IActionResult AccesDenied()
        {
            if (_userSession.HasUser())
            {
                return View();
            }
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
    }
}
