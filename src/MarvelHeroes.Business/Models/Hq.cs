using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes.Business.Models
{
    public class Hq : Entity
    {
        //ID no BD API Marvel
        public int IdMarvel { get; set; }

        //Titulo da comic
        public string Title { get; set; }

        //Descrição da comic
        public string Description { get; set; }

        //Preço de venda da comic
        public decimal? Price { get; set; }

        //Link para imagem da comic
        public string ImageLink { get; set; }

        //Link para wiki da comic
        public string WikiLink { get; set; }
    }
}
