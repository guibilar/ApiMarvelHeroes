namespace MarvelHeroes.Business.Notificacoes
{
    public class Notificacao
    {
        public Notificacao(string tipo, string mensagem)
        {
            Tipo = tipo;
            Mensagem = mensagem;
        }

        public string Tipo { get; }

        public string Mensagem { get; }
    }
}