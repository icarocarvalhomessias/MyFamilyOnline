using FML.Familiares.API.Services.Interface;
using FML.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FML.Familiares.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CasasController : MainController
    {

        private readonly IHouseService _houseService;

        public CasasController(IHouseService houseService)
        {
            _houseService = houseService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Casas(Guid id)
        {
            var resposta = await _houseService.GetHousesByFamilyId(id);
            return CustomResponse(resposta);
        }
    }
}
