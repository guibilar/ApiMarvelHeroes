using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models;
using MarvelHeroes.Business.Models.Enums;
using MarvelHeroes.Business.Notificacoes;
using MarvelHeroes.Integração.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace MarvelHeroes.Integração.Repository
{
    public class HeroIntegrationRepository : IHeroIntegrationRepository
    {
        private readonly IMarvelClient _marvelClient;
        private readonly INotificator _notificator;
        public HeroIntegrationRepository(IMarvelClient marvelClient, INotificator notificator)
        {
            _marvelClient = marvelClient;
            _notificator = notificator;
        }

        public dynamic ListHeros(int limit, int offset)
        {
            List<Hero> herosList = new List<Hero>();
            dynamic output = new ExpandoObject();

            try
            {
                var unprossedOutput = _marvelClient.GetList("characters", limit, offset);

                int size = Convert.ToInt32(unprossedOutput.data.count);
                int total = Convert.ToInt32(unprossedOutput.data.total);

                for (int j = 0; j < size; j++)
                {
                    Hero hero = new Hero();
                    hero.IdMarvel = unprossedOutput.data.results[j].id;
                    hero.Name = unprossedOutput.data.results[j].name;
                    hero.Description = unprossedOutput.data.results[j].description;
                    hero.ImageLink = $"{unprossedOutput.data.results[j].thumbnail.path}.{unprossedOutput.data.results[j].thumbnail.extension}";
                    if (unprossedOutput.data.results[j].urls.Count > 0)
                        hero.WikiLink = unprossedOutput.data.results[j].urls[0].url;
                    herosList.Add(hero);
                }

                output.list = herosList;
                output.total = total;
                output.size = size;
                output.limit = limit;
                output.offset = offset;
            }
            catch
            {
                _notificator.Resolve(new Notification(type: NotificationType.Erro, message: "An integration error has occur"));
            }

            return output;
        }

        public dynamic GetHero(int idMarvel)
        {
            Hero hero = new Hero();
            try
            {
                var unprossedOutput = _marvelClient.GetObject("characters", idMarvel);

                hero.IdMarvel = unprossedOutput.data.results[0].id;
                hero.Name = unprossedOutput.data.results[0].name;
                hero.Description = unprossedOutput.data.results[0].description;
                hero.ImageLink = $"{unprossedOutput.data.results[0].thumbnail.path}.{unprossedOutput.data.results[0].thumbnail.extension}";
                hero.WikiLink = unprossedOutput.data.results[0].urls[1].url;
            }
            catch
            {
                _notificator.Resolve(new Notification(type: NotificationType.Erro, message: "An integration error has occur"));
            }

            return hero;
        }

    }

}
