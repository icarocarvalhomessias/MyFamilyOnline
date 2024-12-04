using Familia.WebApp.MVC.Controllers;
using FML.WebApp.MVC.Models;
using FML.WebApp.MVC.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FML.WebApp.MVC.Controllers
{
    public class ListaDeDesejosController : MainController
    {
        private readonly IEventoServiceRefit _eventoService;

        public ListaDeDesejosController(IEventoServiceRefit eventoService)
        {
            _eventoService = eventoService;
        }

        public async Task<IActionResult> Index()
        {
            var listaDeDesejos = await _eventoService.GetListaDeDesejos();
            return View(listaDeDesejos);
        }

        public IActionResult Create()
        {
            return View("Edit", new ListaDeDesejos());
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var listaDeDesejos = await _eventoService.GetListaDeDesejos();
            var desejo = listaDeDesejos.FirstOrDefault(d => d.Id == id);
            return View("Edit", desejo);
        }

        public async Task<IActionResult> Save(ListaDeDesejos listaDeDesejos)
        {
            await _eventoService.AddListaDeDesejo(listaDeDesejos);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Guid id, ListaDeDesejos listaDeDesejos)
        {
            await _eventoService.EditListaDeDesejos(listaDeDesejos);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _eventoService.DeleteListaDesejos(id);
            return RedirectToAction("Index");

        }

    }
}
