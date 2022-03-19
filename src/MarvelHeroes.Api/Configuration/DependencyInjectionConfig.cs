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
            services.AddScoped<MarvelHeroesDbContext>();
            services.AddScoped<IHqRepository, HqRepository>();
            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<IMarvelClient, MarvelClient>();
            services.AddScoped<IHeroIntegrationRepository, HeroIntegrationRepository>();
            services.AddScoped<IHqIntegrationRepository, HqIntegrationRepository>();
            services.AddScoped<IHeroService, HeroService>();

            services.AddScoped<INotificator, Notificator>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}