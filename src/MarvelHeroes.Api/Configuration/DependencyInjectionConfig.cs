using MarvelHeroes.Api.Extensions;
using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Notificacoes;
using MarvelHeroes.Business.Services;
using MarvelHeroes.Data.Context;
using MarvelHeroes.Data.Repository;
using MarvelHeroes.Integração.Helpers;
using MarvelHeroes.Integração.Interfaces;
using MarvelHeroes.Integração.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MarvelHeroes.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IQuadrinhoRepository, QuadrinhoRepository>();
            services.AddScoped<IPersonagemRepository, PersonagemRepository>();
            services.AddScoped<IMarvelClient, MarvelClient>();
            services.AddScoped<IPersonagemIntegracaoRepository, PersonagemIntegracaoRepository>();
            services.AddScoped<IQuadrinhoIntegracaoRepository, QuadrinhoIntegracaoRepository>();
            services.AddScoped<IPersonagemService, PersonagemService>();

            services.AddScoped<INotificador, Notificador>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}