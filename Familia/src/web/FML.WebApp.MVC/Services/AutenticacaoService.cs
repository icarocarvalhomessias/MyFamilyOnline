﻿using FML.Core.Communication;
using FML.Core.Data;
using FML.WebApp.MVC.Extensions;
using FML.WebApp.MVC.Services.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FML.WebApp.MVC.Services
{
    public class AutenticacaoService : Service, IAutenticacaoService
    {
        private readonly HttpClient _httpClient;
        private readonly IAspNetUser _user;
        public AutenticacaoService(HttpClient httpClient,
                                   IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
        {
            var loginContent = ObterConteudo(usuarioLogin);

            var response = await _httpClient.PostAsync("/api/identidade/autenticar", loginContent);

            var teste = await response.Content.ReadAsStringAsync();

            try
            {
                if (!TratarErrosResponse(response))
                {
                    return new UsuarioRespostaLogin
                    {
                        ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                    };
                }
            }
            catch
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }

        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
        {
            var registroContent = ObterConteudo(usuarioRegistro);

            try
            {
                var response = await _httpClient.PostAsync("/api/identidade/nova-conta", registroContent);

                if (!TratarErrosResponse(response))
                {
                    return new UsuarioRespostaLogin
                    {
                        ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                    };
                }

                return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
            }
            catch (HttpRequestException ex)
            {
                throw;
            }
        }
    }
}
