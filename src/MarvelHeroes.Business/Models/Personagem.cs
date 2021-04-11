using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes.Business.Models
{
    public class Personagem : Entity
    {
        //ID no BD API Marvel
        public int Id_marvel { get; set; }
        //Nome do personagem
        public string Nome { get; set; }
        //Descrição do personagem
        public string Descricao { get; set; }
        //Link para imagem do personagem
        public string Pic_url { get; set; }
        //Link para a Wiki do personagem
        public string Wiki_url { get; set; }
    }
}
