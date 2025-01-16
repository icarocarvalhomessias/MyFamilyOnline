using FML.Core.Mediator;
using FML.Familiares.API.Models;
using FML.Familiares.API.Services.Interface;
using FML.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace FML.Familiares.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FamiliaresController : MainController
    {

        private readonly IRelativeService _relativeService;
        private readonly IMediatorHandler _mediator;

        public FamiliaresController(IRelativeService relativeService,
            IMediatorHandler mediator)
        {
            _relativeService = relativeService;
            _mediator = mediator;
        }

        [HttpDelete("{relativeId:guid}")]
        public async Task<IActionResult> RemoveRelative(Guid relativeId)
        {
            var result = await _relativeService.RemoveRelative(relativeId);

            if (!result)
            {
                AdicionarErroProcessamento("Erro ao remover o parente.");
                return CustomResponse();
            }

            return CustomResponse();
        }


        [HttpGet("{relativeId:guid}")]
        public async Task<IActionResult> GetRelativeById(Guid relativeId)
        {
            var resposta = await _relativeService.GetRelativeById(relativeId);
            return CustomResponse(resposta);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetRelatives()
        {
            var resposta = await _relativeService.GetRelatives();
            return CustomResponse(resposta);
        }

        [HttpPost("carga-inicial")]
        public async Task<IActionResult> CargaInicial()
        {
            var resposta = await _relativeService.CargaInicial();
            return CustomResponse(resposta);
        }

    }
}
