using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes.Business.Models
{
    public class Quadrinho : Entity
    {
        //ID no BD API Marvel
        public int IdMarvel { get; set; }

        //Titulo da comic
        public string Titulo { get; set; }

        //Descrição da comic
        public string Descricao { get; set; }

        //Preço de venda da comic
        public decimal? Preco { get; set; }

        //Link para imagem da comic
        public string LinkImagem { get; set; }

        //Link para wiki da comic
        public string LinkWiki { get; set; }
    }
}
