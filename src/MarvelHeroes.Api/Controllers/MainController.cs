using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MarvelHeroes.Api.ViewModels;
using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models.Enums;
using MarvelHeroes.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MarvelHeroes.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificator _notificator;
        private readonly IMapper _mapper;
        public readonly IUser AppUser;

        protected Guid UserId { get; set; }
        protected bool AuthenicatedUser { get; set; }

        protected MainController(INotificator notificator, 
                                 IUser appUser, IMapper mapper)
        {
            _notificator = notificator;
            _mapper = mapper;
            AppUser = appUser;

            if (appUser.IsAuthenticated())
            {
                UserId = appUser.GetUserId();
                AuthenicatedUser = true;
            }
        }

        protected bool ValidOperation()
        {
            return !_notificator.HasNotifications();
        }

        protected ActionResult CustomResponse(object result = null, int http_code = 400)
        {
            if (ValidOperation())
            {
                return Ok(new
                {
                    http_code = 200,
                    success = true,
                    return_time = DateTime.Now,
                    result = result
                });
            }

            if (http_code == 404)
            {
                return NotFound(new
                {
                    http_code = http_code,
                    success = false,
                    return_time = DateTime.Now,
                    notifications = _mapper.Map<List<NotificationViewModel>>(_notificator.GetNotifications())
                });
            }

            return BadRequest(new
            {
                http_code = http_code,
                success = false,
                return_time = DateTime.Now,
                notifications = _mapper.Map<List<NotificationViewModel>>(_notificator.GetNotifications())
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if(!modelState.IsValid) InvalidModel(modelState);
            return CustomResponse();
        }

        protected void InvalidModel(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                Notificate(NotificationType.Erro, errorMsg);
            }
        }

        protected void Notificate(NotificationType type, string message)
        {
            _notificator.Resolve(new Notification(type, message));
        }

    }
}
