using System.Collections.Generic;
using System.Linq;
using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models.Enums;

namespace MarvelHeroes.Business.Notificacoes
{
    public class Notificator : INotificator
    {
        private List<Notification> _notificacoes;

        public Notificator()
        {
            _notificacoes = new List<Notification>();
        }

        public void Resolve(Notification notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public List<Notification> GetNotifications()
        {
            return _notificacoes;
        }

        public bool HasNotifications()
        {
            return _notificacoes.Any(c => c.Type == NotificationType.Erro || c.Type == NotificationType.Warning);
        }
    }
}