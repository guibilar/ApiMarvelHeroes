using MarvelHeroes.Business.Models;
using MarvelHeroes.Integração.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes.Integração.Repository
{
    public class QuadrinhoIntegracaoRepository : IQuadrinhoIntegracaoRepository
    {

        private readonly IMarvelClient _marvelClient;
        public QuadrinhoIntegracaoRepository(IMarvelClient marvelClient)
        {
            _marvelClient = marvelClient;
        }

        public dynamic ListaQuadrinhosDePersonagem(int idMarvelPersonagem, int limite, int offset)
        {
            List<Quadrinho> listaQuadrinhos = new List<Quadrinho>();

            var resultadoBruto = _marvelClient.ObterListagem($"characters/{idMarvelPersonagem}/comics", limite, offset);

            int tamanho = Convert.ToInt32(resultadoBruto.data.count);
            int total = Convert.ToInt32(resultadoBruto.data.total);

            for (int j = 0; j < tamanho; j++)
            {
                Quadrinho quadrinho = new Quadrinho();
                quadrinho.IdMarvel = resultadoBruto.data.results[j].id;
                quadrinho.Titulo = resultadoBruto.data.results[j].title;
                quadrinho.Descricao = resultadoBruto.data.results[j].description;
                quadrinho.LinkImagem = $"{resultadoBruto.data.results[j].thumbnail.path}.{resultadoBruto.data.results[j].thumbnail.extension}";
                quadrinho.Preco = resultadoBruto.data.results[j].prices[0].price;
                if (resultadoBruto.data.results[j].urls.Count > 0)
                    quadrinho.LinkWiki = resultadoBruto.data.results[j].urls[0].url;
                listaQuadrinhos.Add(quadrinho);
            }

            dynamic resultado = new ExpandoObject();
            resultado.lista = listaQuadrinhos;
            resultado.total = total;
            resultado.tamanho = tamanho;
            resultado.limite = limite;
            resultado.offset = offset;

            return resultado;
        }

        public dynamic ObterQuadrinho(int idMarvel)
        {
            var resultadoBruto = _marvelClient.ObterObjeto("comics", idMarvel);

            var quadrinho = new Quadrinho();
            quadrinho.IdMarvel = resultadoBruto.data.results[0].id;
            quadrinho.Titulo = resultadoBruto.data.results[0].title;
            quadrinho.Descricao = resultadoBruto.data.results[0].description;
            quadrinho.Preco = resultadoBruto.data.results[0].prices[0].price;
            quadrinho.LinkImagem = $"{resultadoBruto.data.results[0].thumbnail.path}.{resultadoBruto.data.results[0].thumbnail.extension}";
            quadrinho.LinkWiki = resultadoBruto.data.results[0].urls[0].url;

            return quadrinho;
        }

    }
}
