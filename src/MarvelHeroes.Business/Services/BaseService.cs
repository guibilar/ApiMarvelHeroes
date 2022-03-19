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
        private readonly INotificator _notificator;

        protected BaseService(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected void Notificate(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificate(NotificationType.Erro, error.ErrorMessage);
            }
        }

        protected void Notificate(NotificationType type, string message)
        {
            _notificator.Resolve(new Notification(type, message));
        }

        protected bool Validate<TV, TE>(TV validaton, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validaton.Validate(entity);

            if(validator.IsValid) return true;

            Notificate(validator);

            return false;
        }
    }
}