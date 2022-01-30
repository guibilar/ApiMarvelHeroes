using AutoMapper;
using PigOut.Api.ViewModels;
using PigOut.Business.Models;
using PigOut.Business.Notificacoes;

namespace PigOut.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Notificacao, NotificacaoViewModel>();
        }
    }
}