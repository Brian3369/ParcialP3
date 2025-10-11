using Microsoft.AspNetCore.Mvc;
using ParcialP3.Application.Interfaces;
using ParcialP3.Application.ViewModels;
using ParcialP3.Domain.Entities;
using ParcialP3.Persistence.Interfaces;
using ParcialP3.WEB.Middlewares;

namespace ParcialP3.WEB.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUserSeccion _userSession;

        public UsersController(IUsersRepository usersRepository, IUserSeccion userSeccion)
        {
            _usersRepository = usersRepository;
            _userSession = userSeccion;
        }
        // GET: UsersController
        public async Task<ActionResult> Index()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            var result = await _usersRepository.GetAll();
            if (result != null)
            {
                List<UsersViewModel> users = new List<UsersViewModel>();

                foreach (var item in result)
                {
                    users.Add(new UsersViewModel
                    {
                        ID = item.ID,
                        Nombre = item.Nombre,
                        Usuario = item.Usuario,
                        Email = item.Email,
                        Edad = item.Edad
                    });
                }

                return View(users);
            }
            return View();
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddUsersViewModel user)
        {
            Users users = new Users
            {
                Nombre = user.Nombre,
                Usuario = user.Usuario,
                Password = user.Password,
                Email = user.Email,
                Edad = user.Edad,
                idEstatus = user.idEstatus
            };

            try
            {
                var result = await _usersRepository.Save(users);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "No se pudo crear Usuario";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _usersRepository.GetEntityBy(id);
            var users = new UpdateUsersViewModel
            {
                ID = result.ID,
                Nombre = result.Nombre,
                Usuario = result.Usuario,
                Password = result.Password,
                Email = result.Email,
                Edad = result.Edad,
                idEstatus = result.idEstatus
            };
            return View(users);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateUsersViewModel users)
        {
            Users user = new Users
            {
                ID = users.ID,
                Nombre = users.Nombre,
                Usuario = users.Usuario,
                Password = users.Password,
                Email = users.Email,
                Edad = users.Edad,
                idEstatus = users.idEstatus
            };
            try
            {
                var result = await _usersRepository.Update(user);
                if (!result)
                {
                    ViewBag.Message = "No se pudo actualizar Usuario";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _usersRepository.GetEntityBy(id);
            var users = new RemoveUserViewModel
            {
                ID = result.ID,
                Nombre = result.Nombre,
                Usuario = result.Usuario,
                Email = result.Email,
                Edad = result.Edad,
                idEstatus = result.idEstatus
            };
            return View(users);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(RemoveUserViewModel users)
        {
            try
            {
                int id = users.ID;
                var result = await _usersRepository.RemoveById(id);
                if (!result)
                {
                    ViewBag.Message = "No se pudo eliminar Usuario";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
