using System;
using System.ComponentModel.DataAnnotations;

namespace MarvelHeroes.Api.ViewModels
{
    public class PersonagemViewModel : PersonagemIntegracaoViewModel
    {
        public Guid Guid { get; set; }
    }
}