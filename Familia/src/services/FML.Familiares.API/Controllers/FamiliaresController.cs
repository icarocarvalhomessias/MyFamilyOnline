using FML.Core.Mediator;
using FML.Familiares.API.Application.Commands;
using FML.Familiares.API.Services.Interface;
using FML.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet]
        public async Task<IActionResult> GetRelatives()
        {
            var resposta = await _relativeService.GetRelatives();
            return CustomResponse(resposta);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarRelative()
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var resultado = await _mediator.EnviarComando
                (new RegistrarFamiliarCommand(Guid.NewGuid(), "TESTEEEEEEEE", "icaro@example.com", DateTime.Parse("14/06/1991"), Gender.Male));


            return CustomResponse(resultado);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Familiar relative)
        {
            await _relativeService.Update(relative);
            return CustomResponse(relative);
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

        [HttpPost("carga-inicial")]
        public async Task<IActionResult> CargaInicial()
        {
            var resposta = await _relativeService.Add();
            return CustomResponse(resposta);
        }

    }
}
