using Familia.WebApp.MVC.Controllers;
using FML.Core.Data;
using FML.WebApp.MVC.Services.Interface;
using FML.WebApp.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using FML.MessageBus;
using FML.Core.Messages.Integrations;
using Microsoft.AspNetCore.Identity;
using System.IO;

namespace FML.WebApp.MVC.Controllers
{
    [Authorize]
    public class FamiliaController : MainController
    {
        private readonly IFamiliaService _familiaService;
        private readonly IMessageBus _bus;
        private readonly IAspNetUser _aspNetUser;

        public FamiliaController(IFamiliaService familiaService, IMessageBus bus, IAspNetUser aspNetUser)
        {
            _familiaService = familiaService;
            _bus = bus;
            _aspNetUser = aspNetUser;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await Table();
        }

        [HttpGet]
        public IActionResult FamilyTree()
        {
            return View("FamilyTree");
        }

        public async Task<IActionResult> Table()
        {
            return View("Table", await GetRelatives());
        }

        /// <summary>
        /// Gets the family tree nodes as JSON.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetFamilyTreeNodes()
        {
            var relatives = await GetRelatives();

            var nodes = relatives.AsParallel().Select(relative => new
            {
                id = relative.Id,
                pids = relative.Spouse.HasValue ? new List<Guid> { relative.Spouse.Value } : new List<Guid>(),
                name = $"{relative.FirstName} {relative.LastName}",
                gender = relative.Gender.ToString().ToLower(),
                mid = relative.MotherId,
                fid = relative.FatherId,
                img = relative.FotoPerfil ?? "https://cdn.balkan.app/shared/5.jpg" // Replace with actual image path if available
            }).ToList();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                MaxDepth = 64,
                Converters = { new JsonStringEnumConverter() }
            };

            return Json(nodes, options);
        }

        /// <summary>
        /// Displays the edit view for a relative.
        /// </summary>
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

        private async Task<ResponseMessage> AtualizaParent(Relative relative, Stream? fotoFile, string? filename)
        {

            var FotoFileBase64 = StringsHelper.ConvertStreamToBase64(fotoFile);

            var familiarAtualizadoIntegrationEvent = new FamiliarAtualizadoIntegrationEvent(
                relative.Id,
                relative.FirstName,
                relative.LastName,
                relative.FamilyId,
                relative.HouseId,
                relative.FatherId ?? Guid.Empty,
                relative.MotherId ?? Guid.Empty,
                relative.LinkName ?? string.Empty,
                string.Empty,
                relative.SecretSanta,
                relative.Email ?? string.Empty,
                relative.BirthDate,
                relative.IsActive,
                relative.IsAlive,
                relative.Patriarch,
                relative.Matriarch,
                relative.DeathDate ?? DateTime.MinValue,
                relative.Gender.ToString(),
                FotoFileBase64,
                filename
            );


            try
            {
                return await _bus.RequestAsync<FamiliarAtualizadoIntegrationEvent, ResponseMessage>(familiarAtualizadoIntegrationEvent);
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("Task was canceled: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Relative model, IFormFile FotoFile)
        {
            if (!ModelState.IsValid)
            {
                LogModelErrors();
                await PopulateDropDownLists();
                return View("Create", model);
            }


            if (FotoFile is null)
            {
                await AtualizaParent(model, null, null);
                return RedirectToAction("Index");
            }

            await AtualizaParent(model, FotoFile.OpenReadStream(), FotoFile.FileName);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            var relatives = await GetRelatives();
            var homens = relatives.Where(x => x.Gender == Gender.Male).ToList();
            var mulheres = relatives.Where(x => x.Gender == Gender.Female).ToList();

            ViewBag.Homens = new SelectList(homens, "Id", "FullName");
            ViewBag.Mulheres = new SelectList(mulheres, "Id", "FullName");

            await PopulateDropDownLists();

            var relative = new Relative();

            return View("Create", relative);
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

        public async Task<IActionResult> RemoveRelative(Relative relative)
        {
            await _familiaService.RemoveRelative(relative.Id);
            return RedirectToAction("Index");
        }

        
        private async Task PopulateDropDownLists()
        {
            var family = await GetRelatives();
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

        private async Task<List<Relative>> GetRelatives()
        {
            return await _familiaService.GetRelatives(Constantes.FamiliaCarvalhoId);
        }
    }
}
