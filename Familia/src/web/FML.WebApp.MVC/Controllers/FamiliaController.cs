using FML.WebApp.MVC.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Index(Guid familyId)
        {
            familyId = Guid.Parse("417d7e43-fe2f-44d6-a8c4-4070e841ad53");
            var relatives = await _familiaService.GetRelativeByFamilyId(familyId);
            return View(relatives);
        }

        public async Task<IActionResult> Create()
        {
            var familyId = Guid.Parse("417d7e43-fe2f-44d6-a8c4-4070e841ad53");
            var relatives = await _familiaService.GetRelativeByFamilyId(familyId);
            var homens = relatives.Where(x => x.Gender == Gender.Male).ToList();
            var mulheres = relatives.Where(x => x.Gender == Gender.Female).ToList();

            ViewBag.Homens = new SelectList(homens, "Id", "FullName");
            ViewBag.Mulheres = new SelectList(mulheres, "Id", "FullName");

            await PopulateDropDownLists(familyId);

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

            await PopulateDropDownLists(relative.FamilyId);
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
            await PopulateDropDownLists(relative.FamilyId);

            return View("Create", relative);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Relative model)
        {
            if (!ModelState.IsValid)
            {
                LogModelErrors();
                await PopulateDropDownLists(model.FamilyId);
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

        private async Task PopulateDropDownLists(Guid familyId)
        {
            var family = await _familiaService.GetRelativeByFamilyId(familyId);
            var homens = family.Where(x => x.Gender == Gender.Male).ToList();
            var mulheres = family.Where(x => x.Gender == Gender.Female).ToList();

            ViewBag.Homens = new SelectList(homens, "Id", "FirstName");
            ViewBag.Mulheres = new SelectList(mulheres, "Id", "FirstName");

            var familias = await _familiaService.GetFamilies();
            var houses = await _familiaService.GetHousesByFamilyId(familyId);

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
