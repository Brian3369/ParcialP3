using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ParcialP3.Application.Interfaces;
using ParcialP3.WEB.Middlewares;

namespace ParcialP3.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserSeccion _userSession;

        public HomeController(ILogger<HomeController> logger, IUserSeccion userSeccion)
        {
            _logger = logger;
            _userSession = userSeccion;
        }

        public IActionResult Index()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View();
        }

        public IActionResult Privacy()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View();
        }
    }
}
