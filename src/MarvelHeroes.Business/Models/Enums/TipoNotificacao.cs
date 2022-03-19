using System.ComponentModel;

namespace MarvelHeroes.Business.Models.Enums
{
    public enum TipoNotificacao
    {
        [Description("Erro")]
        Erro = 0,
        [Description("Aviso")]
        Aviso = 1,
        [Description("Info")]
        Info = 2
    }
}
