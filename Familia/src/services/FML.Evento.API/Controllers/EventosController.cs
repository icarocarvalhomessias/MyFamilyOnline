using Microsoft.AspNetCore.Mvc;
using FML.Evento.API.Services.Interface;


namespace FML.Evento.API.Controllers
{
    [Route("api/eventos")]
    public class EventosController : MainController
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> RealizarAmigoOculto()
        {
            var resultado = await Task.Run(() => _eventoService.RealizarAmigoOculto(Guid.NewGuid()));
            return CustomResponse(resultado);
        }

        [HttpGet]
        [Route("refaz-amigo-oculto")]
        public async Task<IActionResult> RefazAmigoOculto()
        {
            var resultado = await Task.Run(() => _eventoService.RefazAmigoOculto(Guid.NewGuid()));
            return CustomResponse(resultado);
        }

        [HttpGet]
        [Route("parentes")]
        public async Task<IActionResult> ObterTodos()
        {
            var resultado = _eventoService.ObterTodos();
            return CustomResponse(resultado);
        }
    }
}
