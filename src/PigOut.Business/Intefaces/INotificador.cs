using System.Collections.Generic;
using PigOut.Business.Notificacoes;

namespace PigOut.Business.Intefaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Resolver(Notificacao notificacao);
    }
}