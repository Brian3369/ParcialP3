using Microsoft.AspNetCore.Mvc;
using ParcialP3.Application.Interfaces;
using ParcialP3.Application.ViewModels.Inmuebles;
using ParcialP3.Domain.Entities;
using ParcialP3.Persistence.Interfaces;

namespace ParcialP3.WEB.Controllers
{
    public class InmueblesController : Controller
    {
        private readonly IInmueblesRepository _inmueblesRepository;
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly ICondicionRepository _condicionRepository;
        private readonly ICiudadesRepository _ciudadesRepository;
        private readonly IUserSeccion _userSession;

        public InmueblesController(
            IInmueblesRepository inmueblesRepository,
            ITipoPropiedadRepository tipoPropiedadRepository,
            ICondicionRepository condicionRepository,
            ICiudadesRepository ciudadesRepository,
            IUserSeccion userSeccion)
        {
            _inmueblesRepository = inmueblesRepository;
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _condicionRepository = condicionRepository;
            _ciudadesRepository = ciudadesRepository;
            _userSession = userSeccion;
        }
        // GET: UsersController
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
                        Activo = item.Activo
                    });
                }

                return View(inmuebles);
            }
            return View();
        }

        public async Task<ActionResult> Create()
        {       
            var vm = new AddInmueblesViewModel
            {
                TipoPropiedad = await _tipoPropiedadRepository.GetAll(),
                Condicion = await _condicionRepository.GetAll(),
                Ciudades = await _ciudadesRepository.GetAll(),
                Activo = true
            };
            return View(vm);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddInmueblesViewModel inmueble)
        {
            if (!ModelState.IsValid)
            {
                // Re-cargar listas para que el formulario no se quede vacío
                inmueble.TipoPropiedad = await _tipoPropiedadRepository.GetAll();
                inmueble.Condicion = await _condicionRepository.GetAll();
                inmueble.Ciudades = await _ciudadesRepository.GetAll();
                return View(inmueble);
            }

            var entidad = new Inmuebles
            {
                NombreInmueble = inmueble.NombreInmueble,
                Direccion = inmueble.Direccion,
                TipoPropiedadId = inmueble.TipoPropiedadId,
                CondicionId = inmueble.CondicionId,
                CiudadesId = inmueble.CiudadesId,
                Precio = inmueble.Precio,
                Habitacion = inmueble.Habitacion,
                Baños = inmueble.Baños,
                Descripcion = inmueble.Descripcion,
                TipoNegocio = inmueble.TipoNegocio,
                Activo = inmueble.Activo
            };

            try
            {
                var result = await _inmueblesRepository.Save(entidad);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Message = "No se pudo crear inmueble";
                inmueble.TipoPropiedad = await _tipoPropiedadRepository.GetAll();
                inmueble.Condicion = await _condicionRepository.GetAll();
                inmueble.Ciudades = await _ciudadesRepository.GetAll();
                return View(inmueble);
            }
            catch
            {
                inmueble.TipoPropiedad = await _tipoPropiedadRepository.GetAll();
                inmueble.Condicion = await _condicionRepository.GetAll();
                inmueble.Ciudades = await _ciudadesRepository.GetAll();
                return View(inmueble);
            }
        }

        // GET: UsersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _inmueblesRepository.GetEntityBy(id);
            var users = new InmueblesViewModel
            {
                NombreInmueble = result.NombreInmueble,
                Direccion = result.Direccion,
                TipoPropiedadId = result.TipoPropiedadId,
                CondicionId = result.CondicionId,
                CiudadesId = result.CiudadesId,
                Precio = result.Precio,
                Habitacion = result.Habitacion,
                Baños = result.Baños,
                Descripcion = result.Descripcion,
                TipoNegocio = result.TipoNegocio,
                Activo = result.Activo
            };
            return View(users);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(InmueblesViewModel inmueble)
        {
            Inmuebles inmuebles = new Inmuebles
            {
                Id = inmueble.Id,
                NombreInmueble = inmueble.NombreInmueble,
                Direccion = inmueble.Direccion,
                TipoPropiedadId = inmueble.TipoPropiedadId,
                CondicionId = inmueble.CondicionId,
                CiudadesId = inmueble.CiudadesId,
                Precio = inmueble.Precio,
                Habitacion = inmueble.Habitacion,
                Baños = inmueble.Baños,
                Descripcion = inmueble.Descripcion,
                TipoNegocio = inmueble.TipoNegocio,
                Activo = inmueble.Activo
            };
            try
            {
                var result = await _inmueblesRepository.Update(inmuebles);
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

        public async Task<ActionResult> Delete(int id)
        {
            var result = await _inmueblesRepository.GetEntityBy(id);
            var users = new InmueblesViewModel
            {
                Id = result.Id,
                NombreInmueble = result.NombreInmueble,
                Direccion = result.Direccion,
                TipoPropiedadId = result.TipoPropiedadId,
                CondicionId = result.CondicionId,
                CiudadesId = result.CiudadesId,
                Precio = result.Precio,
                Habitacion = result.Habitacion,
                Baños = result.Baños,
                Descripcion = result.Descripcion,
                TipoNegocio = result.TipoNegocio,
                Activo = result.Activo
            };
            return View(users);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(InmueblesViewModel inmuebles)
        {
            try
            {
                int id = inmuebles.Id;
                var result = await _inmueblesRepository.RemoveById(id);
                if (!result)
                {
                    ViewBag.Message = "No se pudo eliminar inmueble";
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
