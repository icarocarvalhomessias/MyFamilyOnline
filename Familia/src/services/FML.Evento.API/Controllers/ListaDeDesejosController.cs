using FML.Evento.API.Data.Entities;
using FML.Evento.API.Models;
using FML.Evento.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FML.Evento.API.Controllers
{
    [Route("api/lista-de-desejos")]

    public class ListaDeDesejosController : MainController
    {
        private readonly IEventoService _eventoService;

        public ListaDeDesejosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }


        [HttpGet]
        public async Task<IActionResult> ListaDeDesejos()
        {
            var listaDeDesejos = await _eventoService.ListaDeDesejos();
            var listaDeDesejoModels = listaDeDesejos.Select(desejo => new ListaDeDesejoModel
            {
                Nome = desejo.Nome,
                Descricao = desejo.Descricao,
                Preco = desejo.Preco,
                Link = desejo.Link,
                Loja = desejo.Loja,
                Observacao = desejo.Observacao,
                ParenteId = desejo.ParenteId
            });

            return CustomResponse(listaDeDesejoModels);
        }


        [HttpPost]
        public async Task<IActionResult> AddListaDeDesejos(DesejoModel desejoModel)
        {
            var desejo = new ListaDeDesejos
            {
                Nome = desejoModel.Nome,
                Descricao = desejoModel.Descricao,
                Preco = desejoModel.Preco,
                Link = desejoModel.Link,
                Loja = desejoModel.Loja,
                Observacao = desejoModel.Observacao,
                ParenteId = desejoModel.ParenteId
            };

            return CustomResponse(await _eventoService.AddListaDeDesejos(desejo));
        }

    }
}
