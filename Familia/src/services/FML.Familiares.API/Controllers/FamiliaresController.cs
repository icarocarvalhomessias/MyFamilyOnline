using FML.Core.Data;
using FML.Core.Mediator;
using FML.Familiares.API.Application.Commands;
using FML.Familiares.API.Models;
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


        [HttpPost]
        public async Task<IActionResult> AtualizaRelative([FromBody] UpdateRelativeModel updateRelativeModel, CancellationToken cancellationToken)
        {
            if (updateRelativeModel == null)
            {
                return BadRequest("Relative cannot be null");
            }

            await _relativeService.Update(updateRelativeModel, cancellationToken);
            return NoContent();
        }



        [HttpGet]
        public async Task<IActionResult> GetRelatives()
        {
            if (updateRelativeModel == null)
            {
                return BadRequest("Relative cannot be null");
            }

            await _relativeService.Update(updateRelativeModel, cancellationToken);
            return NoContent();
        }

        //[HttpPut]
        //public async Task<IActionResult> UpdateRelative(Relative relative)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var result = await _relativeService.Update(relative);
        //    if (!result)
        //    {
        //        return BadRequest("Failed to update relative.");
        //    }

        //    return Ok();
        //}


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
