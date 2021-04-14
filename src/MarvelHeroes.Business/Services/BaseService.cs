using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models;
using MarvelHeroes.Business.Notificacoes;
using FluentValidation;
using FluentValidation.Results;
using MarvelHeroes.Business.Models.Enums;

namespace MarvelHeroes.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(TipoNotificacao.Erro, error.ErrorMessage);
            }
        }

        protected void Notificar(TipoNotificacao tipo, string mensagem)
        {
            _notificador.Resolver(new Notificacao(tipo, mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if(validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}