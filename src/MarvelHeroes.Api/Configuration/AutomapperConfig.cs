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
            CreateMap<Personagem, PersonagemViewModel>().ReverseMap();
            CreateMap<Personagem, PersonagemIntegracaoViewModel>().ReverseMap();
            CreateMap<Quadrinho, QuadrinhoViewModel>().ReverseMap();
            CreateMap<Quadrinho, QuadrinhontegracaoViewModel>().ReverseMap();
            CreateMap<Notificacao, NotificacaoViewModel>();
        }
    }
}