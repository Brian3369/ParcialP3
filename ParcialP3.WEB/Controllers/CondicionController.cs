using Microsoft.AspNetCore.Mvc;
using ParcialP3.Application.Interfaces;
using ParcialP3.Application.ViewModels.Condicion;
using ParcialP3.Domain.Entities;
using ParcialP3.Persistence.Interfaces;

namespace ParcialP3.WEB.Controllers
{
    public class CondicionController : Controller
    {
        private readonly ICondicionRepository _condicionRepository;
        private readonly IUserSeccion _userSession;

        public CondicionController(ICondicionRepository condicionRepository, IUserSeccion userSeccion)
        {
            _condicionRepository = condicionRepository;
            _userSession = userSeccion;
        }

        // GET: CondicionController
        public async Task<ActionResult> Index()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var result = await _condicionRepository.GetAll();
            if (result != null)
            {
                List<CondicionViewModel> condiciones = new List<CondicionViewModel>();

                foreach (var item in result)
                {
                    condiciones.Add(new CondicionViewModel
                    {
                        Id = item.Id,
                        DESCRIPCION = item.DESCRIPCION,
                        Activo = item.Activo
                    });
                }

                return View(condiciones);
            }
            return View();
        }

        // GET: CondicionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CondicionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CondicionViewModel condicion)
        {
            var entity = new Condicion
            {
                DESCRIPCION = condicion.DESCRIPCION,
                Activo = condicion.Activo
            };

            try
            {
                var result = await _condicionRepository.Save(entity);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Message = "No se pudo crear la condición";
                return View(condicion);
            }
            catch
            {
                return View(condicion);
            }
        }

        // GET: CondicionController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _condicionRepository.GetEntityBy(id);
            var model = new CondicionViewModel
            {
                Id = result.Id,
                DESCRIPCION = result.DESCRIPCION,
                Activo = result.Activo
            };
            return View(model);
        }

        // POST: CondicionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CondicionViewModel condicion)
        {
            var entity = new Condicion
            {
                Id = condicion.Id,
                DESCRIPCION = condicion.DESCRIPCION,
                Activo = condicion.Activo
            };

            try
            {
                var result = await _condicionRepository.Update(entity);
                if (!result)
                {
                    ViewBag.Message = "No se pudo actualizar la condición";
                    return View(condicion);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(condicion);
            }
        }

        // GET: CondicionController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _condicionRepository.GetEntityBy(id);
            var model = new CondicionViewModel
            {
                Id = result.Id,
                DESCRIPCION = result.DESCRIPCION,
                Activo = result.Activo
            };
            return View(model);
        }

        // POST: CondicionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(CondicionViewModel condicion)
        {
            try
            {
                var result = await _condicionRepository.RemoveById(condicion.Id);
                if (!result)
                {
                    ViewBag.Message = "No se pudo eliminar la condición";
                    return View(condicion);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(condicion);
            }
        }
    }
}