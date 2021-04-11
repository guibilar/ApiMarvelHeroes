using System;
using System.ComponentModel.DataAnnotations;

namespace MarvelHeroes.Api.ViewModels
{
    public class QuadrinhoViewModel : QuadrinhontegracaoViewModel
    {
        public Guid Guid { get; set; }

    }
}