using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes.Business.Models
{
    public class Hero : Entity
    {
        //ID no BD API Marvel
        public int IdMarvel { get; set; }
        //Nome do personagem
        public string Name { get; set; }
        //Descrição do personagem
        public string Description { get; set; }
        //Link para imagem do personagem
        public string ImageLink { get; set; }
        //Link para a Wiki do personagem
        public string WikiLink { get; set; }
    }
}
