using MarvelHeroes.Business.Models.Enums;

namespace MarvelHeroes.Business.Notificacoes
{
    public class Notification
    {
        public Notification(NotificationType type, string message)
        {
            Type = type;
            TextType = type.ToString();
            Message = message;
        }

        public NotificationType Type { get; }

        public string TextType { get; set; }

        public string Message { get; }
    }
}