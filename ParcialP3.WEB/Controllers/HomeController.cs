using Microsoft.AspNetCore.Mvc;
using ParcialP3.Application.Interfaces;
using ParcialP3.Application.ViewModels.Inmuebles;
using ParcialP3.Persistence.Interfaces;
using ParcialP3.Persistence.Repositories;
using ParcialP3.WEB.Middlewares;
using System.Diagnostics;

namespace ParcialP3.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInmueblesRepository _inmueblesRepository;
        private readonly IUserSeccion _userSession;

        public HomeController(IInmueblesRepository inmueblesRepository, IUserSeccion userSeccion)
        {
            _inmueblesRepository = inmueblesRepository;
            _userSession = userSeccion;
        }

        public async Task<ActionResult> Index()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            var result = await _inmueblesRepository.GetAllWitchIncludes();
            if (result != null)
            {
                List<InmueblesViewModel> inmuebles = new List<InmueblesViewModel>();

                foreach (var item in result)
                {
                    inmuebles.Add(new InmueblesViewModel
                    {
                        Id = item.Id,
                        Direccion = item.Direccion,
                        NombreInmueble = item.NombreInmueble,
                        TipoPropiedadId = item.TipoPropiedadId,
                        TipoPropiedad = item.TipoPropiedad.DESCRIPCION,
                        CondicionId = item.CondicionId,
                        Condicion = item.Condicion.DESCRIPCION,
                        CiudadesId = item.CiudadesId,
                        Ciudades = item.Ciudades.NOMBRE,
                        Precio = item.Precio,
                        Habitacion = item.Habitacion,
                        Baños = item.Baños,
                        Descripcion = item.Descripcion,
                        TipoNegocio = item.TipoNegocio,
                        Activo = item.Activo,
                        Imagenes = item.Imagenes != null ? item.Imagenes.Select(img => img.ImagenURL).ToList() : new List<string>()
                    });
                }

                return View(inmuebles);
            }
            return View();
        }
    }
}
