using FML.Core.Data;
using FML.WebApp.MVC.Services.Interface;
using FML.WebApp.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FML.WebApp.MVC.Controllers
{
    [Authorize]
    public class FamiliaController : Controller
    {
        private readonly IFamiliaService _familiaService;

        public FamiliaController(IFamiliaService familiaService)
        {
            _familiaService = familiaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var relatives = await _familiaService.GetRelatives();
            var familyTree = OrganizeFamilyTree(relatives);
            return View(familyTree);
        }

        // Método para organizar a lista de Relative em FamilyTreeViewModel
        private FamilyTreeViewModel OrganizeFamilyTree(List<Relative> relatives)
        {
            var TESTE = relatives.Where(x => x.Id != Guid.Parse("1a9cedd7-4493-4c7b-c81c-08dd0d7371ad")
            && x.Id != Guid.Parse("9c6a8126-180b-40b2-65bf-08dd0d73fbc5")).ToList();

            // Implementação do método para organizar a árvore genealógica
            // Esta é uma implementação simplificada e pode precisar de ajustes
            var root = new FamilyTreeViewModel
            {
                Pessoa1 = TESTE.FirstOrDefault(r => r.Patriarch),
                Pessoa2 = TESTE.FirstOrDefault(r => r.Matriarch)
            };

            root.Id = GenerateId(root);

            // Adicionar filhos recursivamente
            AddChildren(root, TESTE);

            return root;
        }

        private void AddChildren(FamilyTreeViewModel parent, List<Relative> relatives)
        {
            if (parent.Pessoa1 is null || parent.Pessoa2 is null)
            {
                return;
            }

            // Filtra os filhos que têm o pai ou a mãe como Pessoa1 ou Pessoa2
            var children = relatives.Where(r =>
                (r.MotherId.HasValue && (r.MotherId == parent.Pessoa1.Id || r.MotherId == parent.Pessoa2.Id)) ||
                (r.FatherId.HasValue && (r.FatherId == parent.Pessoa1.Id || r.FatherId == parent.Pessoa2.Id))
            ).Distinct().ToList();

            foreach (var child in children)
            {
                var childNode = new FamilyTreeViewModel
                {
                    Pessoa1 = child,
                    Pessoa2 = relatives.FirstOrDefault(r => r.Id == child.Spouse) // Exemplo de cônjuge
                };

                childNode.Id = GenerateId(childNode);

                parent.Filhos.Add(childNode);
                AddChildren(childNode, relatives);
            }
        }

        private string GenerateId(FamilyTreeViewModel familyTreeViewModel)
        {
            var id1 = familyTreeViewModel.Pessoa1?.Id.ToString() ?? string.Empty;
            var id2 = familyTreeViewModel.Pessoa2?.Id.ToString() ?? string.Empty;
            return id1 + id2;
        }


        public async Task<IActionResult> Create()
        {
            var relatives = await _familiaService.GetRelatives();
            var homens = relatives.Where(x => x.Gender == Gender.Male).ToList();
            var mulheres = relatives.Where(x => x.Gender == Gender.Female).ToList();

            ViewBag.Homens = new SelectList(homens, "Id", "FullName");
            ViewBag.Mulheres = new SelectList(mulheres, "Id", "FullName");

            await PopulateDropDownLists();

            return View("Create", new Relative());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Relative relative)
        {
            if (ModelState.IsValid)
            {
                await _familiaService.AddRelative(relative);
                return RedirectToAction(nameof(Index), new { familyId = relative.FamilyId });
            }

            await PopulateDropDownLists();
            return View(relative);
        }

        public async Task<IActionResult> ParenteEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var relative = await _familiaService.GetRelativeById(id);
            if (relative == null)
            {
                return NotFound();
            }

            ViewBag.Relative = relative;
            await PopulateDropDownLists();

            return View("Create", relative);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Relative model)
        {
            if (!ModelState.IsValid)
            {
                LogModelErrors();
                await PopulateDropDownLists();
                return View("Create", model);
            }

            await _familiaService.UpdateRelative(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveRelative(Relative relative)
        {
            await _familiaService.RemoveRelative(relative.Id);
            return RedirectToAction("Index");
        }

        private async Task PopulateDropDownLists()
        {
            var family = await _familiaService.GetRelatives();
            var homens = family.Where(x => x.Gender == Gender.Male).ToList();
            var mulheres = family.Where(x => x.Gender == Gender.Female).ToList();

            ViewBag.Homens = new SelectList(homens, "Id", "FirstName");
            ViewBag.Mulheres = new SelectList(mulheres, "Id", "FirstName");

            var familias = await _familiaService.GetFamilies();
            var houses = await _familiaService.GetHousesByFamilyId(familias.First().Id);

            ViewBag.Familias = new SelectList(familias, "Id", "Name");
            ViewBag.Casas = new SelectList(houses, "Id", "Name");
            ViewBag.Spouses = new SelectList(family, "Id", "FirstName");
        }

        private void LogModelErrors()
        {
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Property: {state.Key} Error: {error.ErrorMessage}");
                }
            }
        }
    }
}
