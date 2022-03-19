using AutoMapper;
using MarvelHeroes.Api.ViewModels;
using MarvelHeroes.Business.Models;
using MarvelHeroes.Business.Notificacoes;

namespace MarvelHeroes.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Hero, HeroViewModel>().ReverseMap();
            CreateMap<Hero, HeroIntegrationViewModel>().ReverseMap();
            CreateMap<Hq, HqViewModel>().ReverseMap();
            CreateMap<Hq, HqIntegrationViewModel>().ReverseMap();
            CreateMap<Notification, NotificationViewModel>();
        }
    }
}