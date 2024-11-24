using Familia.WebApp.MVC.Controllers;
using FML.WebApp.MVC.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FML.WebApp.MVC.Controllers
{
    [Authorize]
    public class EventoController : MainController
    {
        private readonly IEventoService _eventoService;


        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RealizarAmigoOculto()
        {
            var resultado = _eventoService.RealizaAmigoOculto().Result;

            resultado = ImproveResults(resultado);

            return View("AmigoOculto", resultado);
        }

        private static List<SecretSantaPair> ImproveResults(List<SecretSantaPair> resultado)
        {
            // Ordena o resultado para que Icaro, Lidia, Natasha e Marcela fiquem em primeiro
            var nomesPrioritarios = new List<string> { "Natasha Brito de Carvalho Messias", "Icaro Brito de Carvalho Messias", "Lidia Mara Pereira Aguiar", "Marcela Brito de Carvalho Messias" };
            resultado = resultado.OrderByDescending(pair => nomesPrioritarios.Contains(pair.MeuNome))
                                 .ThenBy(pair => pair.MeuNome)
                                 .ToList();

            var fileName = $"AmigoOculto_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sorteios", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var writer = new StreamWriter(filePath))
            {
                foreach (var pair in resultado)
                {
                    writer.WriteLine($"Meu Nome: {pair.MeuNome}");
                    writer.WriteLine($"Meu Amigo Oculto: {pair.MeuAmigoOculto}");
                    writer.WriteLine($"Link Nome: {pair.LinkNome}");
                    writer.WriteLine();
                }
            }

            return resultado;
        }

        [HttpPost]
        public IActionResult RefazAmigoOculto()
        {
            var resultado = _eventoService.RefazAmigoOculto().Result;

            resultado = ImproveResults(resultado);

            return View("AmigoOculto", resultado);

        }

    }
}
