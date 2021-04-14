using MarvelHeroes.Business.Models.Enums;

namespace MarvelHeroes.Business.Notificacoes
{
    public class Notificacao
    {
        public Notificacao(TipoNotificacao tipo, string mensagem)
        {
            Tipo = tipo;
            TipoTexto = tipo.ToString();
            Mensagem = mensagem;
        }

        public TipoNotificacao Tipo { get; }

        public string TipoTexto { get; set; }

        public string Mensagem { get; }
    }
}