using FML.Familiares.API.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FML.Familiares.API.Controllers
{
    [Route("api/house")]
    [Authorize]
    public class HouseController : MainController
    {

        private readonly IHouseService _houseService;

        public HouseController(IHouseService houseService)
        {
            _houseService = houseService;
        }

        [HttpGet("/house/family/{id:guid}")]
        public async Task<IActionResult> GetHouseByFamilyId(Guid id)
        {
            var resposta = await _houseService.GetHousesByFamilyId(id);
            return CustomResponse(resposta);
        }
    }
}
