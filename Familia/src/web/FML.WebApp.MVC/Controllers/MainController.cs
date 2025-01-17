using FluentValidation.Results;
using FML.Core.Communication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Familia.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponsePossuiErros(ResponseResult resposta)
        {
            if (resposta != null && resposta.Errors.Mensagens.Any())
            {
                foreach (var mensagem in resposta.Errors.Mensagens)
                {
                    ModelState.AddModelError(string.Empty, mensagem);
                }

                return true;
            }

            return false;
        }

        protected bool ResponsePossuiErros(ValidationResult validationResult)
        {
            if (validationResult != null && validationResult.Errors.Any())
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }

                return true;
            }

            return false;
        }
    }
}
