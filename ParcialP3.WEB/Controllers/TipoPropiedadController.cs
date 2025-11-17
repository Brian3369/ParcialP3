using Microsoft.AspNetCore.Mvc;
using ParcialP3.Application.Interfaces;
using ParcialP3.Application.ViewModels.TipoPropiedad;
using ParcialP3.Domain.Entities;
using ParcialP3.Persistence.Interfaces;

namespace ParcialP3.WEB.Controllers
{
    public class TipoPropiedadController : Controller
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IUserSeccion _userSession;

        public TipoPropiedadController(ITipoPropiedadRepository tipoPropiedadRepository, IUserSeccion userSeccion)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _userSession = userSeccion;
        }

        // GET: TipoPropiedadController
        public async Task<ActionResult> Index()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var result = await _tipoPropiedadRepository.GetAll();
            if (result != null)
            {
                var modelos = new List<TipoPropiedadViewModel>();
                foreach (var item in result)
                {
                    modelos.Add(new TipoPropiedadViewModel
                    {
                        Id = item.Id,
                        DESCRIPCION = item.DESCRIPCION,
                        Activo = item.Activo
                    });
                }

                return View(modelos);
            }
            return View();
        }

        // GET: TipoPropiedadController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPropiedadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TipoPropiedadViewModel tipo)
        {
            var entity = new TipoPropiedad
            {
                DESCRIPCION = tipo.DESCRIPCION,
                Activo = tipo.Activo
            };

            try
            {
                var result = await _tipoPropiedadRepository.Save(entity);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Message = "No se pudo crear el tipo de propiedad";
                return View(tipo);
            }
            catch
            {
                return View(tipo);
            }
        }

        // GET: TipoPropiedadController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _tipoPropiedadRepository.GetEntityBy(id);
            var model = new TipoPropiedadViewModel
            {
                Id = result.Id,
                DESCRIPCION = result.DESCRIPCION,
                Activo = result.Activo
            };
            return View(model);
        }

        // POST: TipoPropiedadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TipoPropiedadViewModel tipo)
        {
            var entity = new TipoPropiedad
            {
                Id = tipo.Id,
                DESCRIPCION = tipo.DESCRIPCION,
                Activo = tipo.Activo
            };

            try
            {
                var result = await _tipoPropiedadRepository.Update(entity);
                if (!result)
                {
                    ViewBag.Message = "No se pudo actualizar el tipo de propiedad";
                    return View(tipo);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(tipo);
            }
        }

        // GET: TipoPropiedadController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _tipoPropiedadRepository.GetEntityBy(id);
            var model = new TipoPropiedadViewModel
            {
                Id = result.Id,
                DESCRIPCION = result.DESCRIPCION,
                Activo = result.Activo
            };
            return View(model);
        }

        // POST: TipoPropiedadController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(TipoPropiedadViewModel tipo)
        {
            try
            {
                var result = await _tipoPropiedadRepository.RemoveById(tipo.Id);
                if (!result)
                {
                    ViewBag.Message = "No se pudo eliminar el tipo de propiedad";
                    return View(tipo);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(tipo);
            }
        }
    }
}