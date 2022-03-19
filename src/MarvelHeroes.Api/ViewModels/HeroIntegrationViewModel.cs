using System;
using System.ComponentModel.DataAnnotations;

namespace MarvelHeroes.Api.ViewModels
{
    public class HeroIntegrationViewModel
    {
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