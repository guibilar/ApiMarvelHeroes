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
    public class PersonagemIntegracaoRepository : IPersonagemIntegracaoRepository
    {
        private readonly IMarvelClient _marvelClient;
        private readonly INotificador _notificador;
        public PersonagemIntegracaoRepository(IMarvelClient marvelClient, INotificador notificador)
        {
            _marvelClient = marvelClient;
            _notificador = notificador;
        }

        public dynamic ListaPersonagens(int limite, int offset)
        {
            List<Personagem> listaDePersonagens = new List<Personagem>();
            dynamic resultado = new ExpandoObject();

            try
            {
                var resultadoBruto = _marvelClient.ObterListagem("characters", limite, offset);

                int tamanho = Convert.ToInt32(resultadoBruto.data.count);
                int total = Convert.ToInt32(resultadoBruto.data.total);

                for (int j = 0; j < tamanho; j++)
                {
                    Personagem personagem = new Personagem();
                    personagem.IdMarvel = resultadoBruto.data.results[j].id;
                    personagem.Nome = resultadoBruto.data.results[j].name;
                    personagem.Descricao = resultadoBruto.data.results[j].description;
                    personagem.LinkImagem = $"{resultadoBruto.data.results[j].thumbnail.path}.{resultadoBruto.data.results[j].thumbnail.extension}";
                    if (resultadoBruto.data.results[j].urls.Count > 0)
                        personagem.LinkWiki = resultadoBruto.data.results[j].urls[0].url;
                    listaDePersonagens.Add(personagem);
                }

                resultado.lista = listaDePersonagens;
                resultado.total = total;
                resultado.tamanho = tamanho;
                resultado.limite = limite;
                resultado.offset = offset;
            }
            catch
            {
                _notificador.Resolver(new Notificacao(tipo: TipoNotificacao.Erro, mensagem: "Um erro de integração ocorreu"));
            }

            return resultado;
        }

        public dynamic ObterPersonagem(int idMarvel)
        {
            Personagem personagem = new Personagem();
            try
            {
                var resultadoBruto = _marvelClient.ObterObjeto("characters", idMarvel);

                personagem.IdMarvel = resultadoBruto.data.results[0].id;
                personagem.Nome = resultadoBruto.data.results[0].name;
                personagem.Descricao = resultadoBruto.data.results[0].description;
                personagem.LinkImagem = $"{resultadoBruto.data.results[0].thumbnail.path}.{resultadoBruto.data.results[0].thumbnail.extension}";
                personagem.LinkWiki = resultadoBruto.data.results[0].urls[1].url;
            }
            catch
            {
                _notificador.Resolver(new Notificacao(tipo: TipoNotificacao.Erro, mensagem: "Um erro de integração ocorreu"));
            }

            return personagem;
        }

    }

}
