using FML.WebApi.Core.Controllers;
using FML.WebApi.Core.Identidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FML.Familiares.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FamiliasController : MainController
    {
        private readonly IFamilyService _familiaService;

        public FamiliasController(IFamilyService familiaService)
        {
            _familiaService = familiaService;
        }

        [HttpPost]
        [ClaimsAuthorize("Familia", "Read/Write")]
        public async Task<IActionResult> AdicionarFamilia(Family family)
        {
            await _familiaService.AddFamily(family);
            return CustomResponse(family);
        }

        [HttpGet]
        [ClaimsAuthorize("Familia", "Read/Write")]
        public async Task<IActionResult> GetFamilies()
        {
            var families = await _familiaService.GetFamilies();
            return CustomResponse(families);
        }
    }
}
