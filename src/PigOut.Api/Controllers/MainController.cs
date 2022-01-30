using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PigOut.Api.ViewModels;
using PigOut.Business.Intefaces;
using PigOut.Business.Models.Enums;
using PigOut.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PigOut.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;

        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

        protected MainController(INotificador notificador,IMapper mapper)
        {
            _notificador = notificador;
            _mapper = mapper;
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

            if (http_code == 404)
            {
                return NotFound(new
                {
                    codigo_http = http_code,
                    sucesso = false,
                    hora_retorno = DateTime.Now,
                    notificacoes = _mapper.Map<List<NotificacaoViewModel>>(_notificador.ObterNotificacoes())
                });
            }

            return BadRequest(new
            {
                codigo_http = http_code,
                sucesso = false,
                hora_retorno = DateTime.Now,
                notificacoes = _mapper.Map<List<NotificacaoViewModel>>(_notificador.ObterNotificacoes())
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
                Notificar(TipoNotificacao.Erro, errorMsg);
            }
        }

        protected void Notificar(TipoNotificacao tipo, string mensagem)
        {
            _notificador.Resolver(new Notificacao(tipo, mensagem));
        }

    }
}
