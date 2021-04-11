using System;
using System.ComponentModel.DataAnnotations;

namespace MarvelHeroes.Api.ViewModels
{
    public class PersonagemIntegracaoViewModel
    {
        public int IdMarvel { get; set; }
        //Nome do personagem
        public string Nome { get; set; }
        //Descrição do personagem
        public string Descricao { get; set; }
        //Link para imagem do personagem
        public string LinkImagem { get; set; }
        //Link para a Wiki do personagem
        public string LinkWiki { get; set; }
    }
}