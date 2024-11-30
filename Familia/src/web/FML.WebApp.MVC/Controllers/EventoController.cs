using Familia.WebApp.MVC.Controllers;
using FML.WebApp.MVC.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> RealizarAmigoOculto()
        {
            var resultado = await _eventoService.RealizaAmigoOculto();

            resultado = MelhoraResposta(resultado);

            return View("Index", resultado);
        }

        private static List<SecretSantaPair> MelhoraResposta(List<SecretSantaPair> resultado)
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
        public async Task<IActionResult> RefazAmigoOculto()
        {
            var resultado = await _eventoService.RefazAmigoOculto();

            resultado = MelhoraResposta(resultado);

            return View("Index", resultado);

        }

    }
}
