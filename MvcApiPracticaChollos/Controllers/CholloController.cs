using Microsoft.AspNetCore.Mvc;
using MvcApiPracticaChollos.Models;
using MvcApiPracticaChollos.Services;

namespace MvcApiPracticaChollos.Controllers
{
    public class CholloController : Controller
    {
        private ServiceChollos service;
        public CholloController(ServiceChollos service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Chollo> chollos = await this.service.GetChollosAsync();
            return View(chollos);
        }

        public async Task<IActionResult> Details(int id)
        {
            Chollo chollo = await this.service.FindChollo(id);
            return View(chollo);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Chollo chollo)
        {
            await this.service.InsertChollo(chollo.IdChollo, chollo.Titulo, chollo.Link, chollo.Descripcion, chollo.Fecha);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
           Chollo chollo =  await this.service.FindChollo(id);
            return View(chollo);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Chollo chollo)
        {
           
            await this.service.UpdateChollo(chollo.IdChollo, chollo.Titulo, chollo.Link, chollo.Descripcion, chollo.Fecha);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteChollo(id);
            return RedirectToAction("Index");
        }
        
    }
}
