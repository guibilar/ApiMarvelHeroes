using System;
using System.ComponentModel.DataAnnotations;

namespace MarvelHeroes.Api.ViewModels
{
    public class HeroViewModel : HeroIntegrationViewModel
    {
        public Guid Guid { get; set; }
    }
}