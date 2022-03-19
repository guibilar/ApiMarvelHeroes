using System.ComponentModel;

namespace MarvelHeroes.Business.Models.Enums
{
    public enum NotificationType
    {
        [Description("Erro")]
        Erro = 0,
        [Description("Warning")]
        Warning = 1,
        [Description("Info")]
        Info = 2
    }
}
