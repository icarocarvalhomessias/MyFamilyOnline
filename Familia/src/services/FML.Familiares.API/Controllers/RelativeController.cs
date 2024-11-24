using FML.Familiares.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FML.Familiares.API.Controllers
{
    [Route("api/relative")]
    public class RelativeController : MainController
    {

        private readonly IRelativeService _relativeService;

        public RelativeController(IRelativeService relativeService)
        {
            _relativeService = relativeService;
        }


        [HttpGet("/family/{id:guid}")]
        public async Task<IActionResult> GetRelativeByFamilyId(Guid id)
        {
            var resposta = await _relativeService.GetRelativeByFamilyId(id);
            return CustomResponse(resposta);
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            var resposta = await _relativeService.Add();
            return CustomResponse(resposta);
        }
    }
}
