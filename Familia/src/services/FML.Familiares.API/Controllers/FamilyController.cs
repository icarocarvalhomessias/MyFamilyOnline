using FML.WebApi.Core.Identidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FML.Familiares.API.Controllers
{
    [Route("api/family")]
    [Authorize]
    public class FamilyController : MainController
    {
        private readonly IFamilyService _familiaService;

        public FamilyController(IFamilyService familiaService)
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

        [HttpGet("/families")]
        public async Task<IActionResult> GetFamilies()
        {
            var families = await _familiaService.GetFamilies();
            return CustomResponse(families);
        }
    }
}
