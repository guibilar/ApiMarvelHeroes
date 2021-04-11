﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MarvelHeroes.Api.ViewModels
{
    public class QuadrinhoViewModel
    {
        public Guid Guid { get; set; }

        //ID no BD API Marvel
        public int Id_marvel { get; set; }

        //Titulo da comic
        public string Titulo { get; set; }

        //Descrição da comic
        public string Descricao { get; set; }

        //Preço de venda da comic
        public decimal? Preco { get; set; }

        //Link para imagem da comic
        public string Pic_url { get; set; }

        //Link para wiki da comic
        public string Wiki_url { get; set; }
    }
}