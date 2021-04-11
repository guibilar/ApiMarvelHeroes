using System;
using System.Linq;
using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MarvelHeroes.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador _notificador;
        public readonly IUser AppUser;

        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

        protected MainController(INotificador notificador, 
                                 IUser appUser)
        {
            _notificador = notificador;
            AppUser = appUser;

            if (appUser.IsAuthenticated())
            {
                UsuarioId = appUser.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null, int http_code = 400)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    codigo_http = 200,
                    sucesso = true,
                    hora_retorno = DateTime.Now,
                    retorno = result
                });
            }

            if(http_code == 404)
            {
                return NotFound(new
                {
                    codigo_http = http_code,
                    sucesso = false,
                    hora_retorno = DateTime.Now,
                    notificacoes = _notificador.ObterNotificacoes()
                });
            }

            return BadRequest(new
            {
                codigo_http = http_code,
                sucesso = false,
                hora_retorno = DateTime.Now,
                notificacoes = _notificador.ObterNotificacoes()
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if(!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Resolver(new Notificacao("Erro", mensagem));
        }

        protected void NotificarAviso(string mensagem)
        {
            _notificador.Resolver(new Notificacao("Aviso", mensagem));
        }

        protected void NotificarInformacao(string mensagem)
        {
            _notificador.Resolver(new Notificacao("Info", mensagem));
        }
    }
}
