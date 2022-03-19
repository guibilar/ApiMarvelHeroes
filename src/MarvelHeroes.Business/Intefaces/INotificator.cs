using System.Collections.Generic;
using MarvelHeroes.Business.Notificacoes;

namespace MarvelHeroes.Business.Intefaces
{
    public interface INotificator
    {
        bool HasNotifications();
        List<Notification> GetNotifications();
        void Resolve(Notification notification);
    }
}