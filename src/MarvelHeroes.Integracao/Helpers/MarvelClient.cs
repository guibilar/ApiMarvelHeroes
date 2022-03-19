using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models.Enums;
using MarvelHeroes.Business.Notificacoes;
using MarvelHeroes.Integração.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace MarvelHeroes.Integração.Helpers
{
    public class MarvelClient : IMarvelClient
    {
        private HttpClient _client { get; set; }
        private string _publicKey { get; } = Environment.GetEnvironmentVariable("publicKey");
        private string _privateKey { get; } = Environment.GetEnvironmentVariable("privateKey");

        private readonly INotificator _notificator;

        public MarvelClient(INotificator notificador)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("marvelApiUrl"));
            _notificator = notificador;
        }

        private string GenerateHash(string carimboDeTempo)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(carimboDeTempo + _privateKey + _publicKey);
            var gerador = MD5.Create();
            byte[] bytesHash = gerador.ComputeHash(bytes);

            return BitConverter.ToString(bytesHash).ToLower().Replace("-", String.Empty);
        }

        public dynamic GetList(string path, int limit, int offset)
        {
            string timeStamp = DateTime.Now.Ticks.ToString();
            HttpResponseMessage response = _client.GetAsync($"{path}?ts={timeStamp}&limit={limit}&offset={offset}&apikey={_publicKey}&hash={GenerateHash(timeStamp)}").Result;

            string content = response.Content.ReadAsStringAsync().Result;

            object output = JsonConvert.DeserializeObject(content);

            Validate(output);

            return output;
        }

        public dynamic GetObject(string path, int idMarvel)
        {
            string timeStamp = DateTime.Now.Ticks.ToString();
            HttpResponseMessage response = _client.GetAsync($"{path}/{idMarvel}?ts={timeStamp}&apikey={_publicKey}&hash={GenerateHash(timeStamp)}").Result;

            string content = response.Content.ReadAsStringAsync().Result;

            object output = JsonConvert.DeserializeObject(content);

            Validate(output);

            return output;
        }

        private void Validate(dynamic resultado)
        {
            switch ((int)resultado.code)
            {
                case 404:
                    {
                        _notificator.Resolve(new Notification(type: NotificationType.Warning, message: "Object not found"));
                        break;
                    }
                case 401:
                    {
                        _notificator.Resolve(new Notification(type: NotificationType.Warning, message: "Invalid Marvel Credentials"));
                        break;
                    }
                case 500:
                    {
                        _notificator.Resolve(new Notification(type: NotificationType.Erro, message: "A integration error has occur"));
                        break;
                    }
            }
        }
    }
}
