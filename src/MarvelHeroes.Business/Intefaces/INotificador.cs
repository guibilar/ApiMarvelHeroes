using System.Collections.Generic;
using MarvelHeroes.Business.Notificacoes;

namespace MarvelHeroes.Business.Intefaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Resolver(Notificacao notificacao);
    }
}