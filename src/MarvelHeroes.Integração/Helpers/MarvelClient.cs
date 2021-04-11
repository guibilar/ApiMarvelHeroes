using MarvelHeroes.Business.Models;
using MarvelHeroes.Integração.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace MarvelHeroes.Integração.Helpers
{
    public class MarvelClient : IMarvelClient
    {
        public HttpClient _client { get; set; }
        public string _chavePublica { get; set; }
        public string _chavePrivada { get; set; }

        public MarvelClient()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("marvelApiUrl"));
            _chavePrivada = Environment.GetEnvironmentVariable("privateKey");
            _chavePublica = Environment.GetEnvironmentVariable("publicKey");
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

            return resultado;
        }

        public dynamic ObterObjeto(string caminho, int idMarvel)
        {
            string carimboDeTempo = DateTime.Now.Ticks.ToString();
            HttpResponseMessage response = _client.GetAsync($"{caminho}/{idMarvel}?ts={carimboDeTempo}&apikey={_chavePublica}&hash={CalculaHash(carimboDeTempo)}").Result;

            string conteudo = response.Content.ReadAsStringAsync().Result;

            object resultado = JsonConvert.DeserializeObject(conteudo);

            return resultado;
        }
    }
}
