using FML.WebApp.MVC.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FML.WebApp.MVC.Services.Interface
{
    public interface IEventoServiceRefit
    {
        [Get("/api/eventos")]
        Task<List<SecretSantaPair>> RealizaAmigoOculto();

        [Get("/api/eventos/refaz-amigo-oculto")]
        Task<List<SecretSantaPair>> RefazAmigoOculto();

        [Post("/api/lista-de-desejos")]
        Task<ListaDeDesejos> AddListaDeDesejo(ListaDeDesejos listaDeDesejos);
        
        [Get("/api/lista-de-desejos")]
        Task<List<ListaDeDesejos>> GetListaDeDesejos();

        [Put("/api/lista-de-desejos")]
        Task<bool> EditListaDeDesejos(ListaDeDesejos listaDeDesejos);

        [Delete("/api/lista-de-desejos/{id}")]
        Task DeleteListaDesejos(Guid id);
    }
}
