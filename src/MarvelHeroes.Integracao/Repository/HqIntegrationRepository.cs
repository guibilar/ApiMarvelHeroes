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
    public class HqIntegrationRepository : IHqIntegrationRepository
    {

        private readonly IMarvelClient _marvelClient;
        private readonly INotificator _notificator;

        public HqIntegrationRepository(IMarvelClient marvelClient, INotificator notificator)
        {
            _marvelClient = marvelClient;
            _notificator = notificator;
        }

        public dynamic GetHeroHq(int idMarvelHero, int limit, int offset)
        {

            List<Hq> hqList = new List<Hq>();
            dynamic output = new ExpandoObject();
            try
            {
                var unprocessedOutput = _marvelClient.GetList($"characters/{idMarvelHero}/comics", limit, offset);

                int size = Convert.ToInt32(unprocessedOutput.data.count);
                int total = Convert.ToInt32(unprocessedOutput.data.total);

                for (int j = 0; j < size; j++)
                {
                    Hq hq = new Hq();
                    hq.IdMarvel = unprocessedOutput.data.results[j].id;
                    hq.Title = unprocessedOutput.data.results[j].title;
                    hq.Description = unprocessedOutput.data.results[j].description;
                    hq.ImageLink = $"{unprocessedOutput.data.results[j].thumbnail.path}.{unprocessedOutput.data.results[j].thumbnail.extension}";
                    hq.Price = unprocessedOutput.data.results[j].prices[0].price;
                    if (unprocessedOutput.data.results[j].urls.Count > 0)
                        hq.WikiLink = unprocessedOutput.data.results[j].urls[0].url;
                    hqList.Add(hq);
                }

                output.list = hqList;
                output.total = total;
                output.size = size;
                output.limit = limit;
                output.offset = offset;
            }
            catch
            {
                _notificator.Resolve(new Notification(NotificationType.Erro, message: "An integration error has occur"));
            }

            return output;
        }

        public dynamic GetHq(int idMarvel)
        {
            var hq = new Hq();
            try
            {
                var unprocessedOutput = _marvelClient.GetObject("comics", idMarvel);


                hq.IdMarvel = unprocessedOutput.data.results[0].id;
                hq.Title = unprocessedOutput.data.results[0].title;
                hq.Description = unprocessedOutput.data.results[0].description;
                hq.Price = unprocessedOutput.data.results[0].prices[0].price;
                hq.ImageLink = $"{unprocessedOutput.data.results[0].thumbnail.path}.{unprocessedOutput.data.results[0].thumbnail.extension}";
                hq.WikiLink = unprocessedOutput.data.results[0].urls[0].url;
            }
            catch
            {
                _notificator.Resolve(new Notification(NotificationType.Erro, message: "An integration error has occur"));
            }

            return hq;
        }

    }
}
