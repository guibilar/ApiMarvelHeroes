using System.Collections.Generic;
using System.Linq;
using PigOut.Business.Intefaces;
using PigOut.Business.Models.Enums;

namespace PigOut.Business.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacoes;

        public Notificador()
        {
            _notificacoes = new List<Notificacao>();
        }

        public void Resolver(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public bool TemNotificacao()
        {
            return _notificacoes.Any(c => c.Tipo == TipoNotificacao.Erro || c.Tipo == TipoNotificacao.Aviso);
        }
    }
}