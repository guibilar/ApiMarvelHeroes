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
        private string _chavePublica { get; } = Environment.GetEnvironmentVariable("publicKey");
        private string _chavePrivada { get; } = Environment.GetEnvironmentVariable("privateKey");

        private readonly INotificador _notificador;

        public MarvelClient(INotificador notificador)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("marvelApiUrl"));
            _notificador = notificador;
        }

        private string CalculaHash(string carimboDeTempo)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(carimboDeTempo + _chavePrivada + _chavePublica);
            var gerador = MD5.Create();
            byte[] bytesHash = gerador.ComputeHash(bytes);

            return BitConverter.ToString(bytesHash).ToLower().Replace("-", String.Empty);
        }

        public dynamic ObterListagem(string caminho, int limite, int offset)
        {
            string carimboDeTempo = DateTime.Now.Ticks.ToString();
            HttpResponseMessage response = _client.GetAsync($"{caminho}?ts={carimboDeTempo}&limit={limite}&offset={offset}&apikey={_chavePublica}&hash={CalculaHash(carimboDeTempo)}").Result;

            string conteudo = response.Content.ReadAsStringAsync().Result;

            object resultado = JsonConvert.DeserializeObject(conteudo);

            VerificaException(resultado);

            return resultado;
        }

        public dynamic ObterObjeto(string caminho, int idMarvel)
        {
            string carimboDeTempo = DateTime.Now.Ticks.ToString();
            HttpResponseMessage response = _client.GetAsync($"{caminho}/{idMarvel}?ts={carimboDeTempo}&apikey={_chavePublica}&hash={CalculaHash(carimboDeTempo)}").Result;

            string conteudo = response.Content.ReadAsStringAsync().Result;

            object resultado = JsonConvert.DeserializeObject(conteudo);

            VerificaException(resultado);

            return resultado;
        }

        private void VerificaException(dynamic resultado)
        {
            switch ((int)resultado.code)
            {
                case 404:
                    {
                        _notificador.Resolver(new Notificacao(tipo: TipoNotificacao.Aviso, mensagem: "Objeto não encontrado"));
                        break;
                    }
                case 401:
                    {
                        _notificador.Resolver(new Notificacao(tipo: TipoNotificacao.Aviso, mensagem: "Suas credenciais Marvel não são válidas"));
                        break;
                    }
                case 500:
                    {
                        _notificador.Resolver(new Notificacao(tipo: TipoNotificacao.Erro, mensagem: "Um erro de integração ocorreu"));
                        break;
                    }
            }
        }
    }
}
