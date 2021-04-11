﻿using FluentValidation;

namespace MarvelHeroes.Business.Models.Validations
{
    public class PersonagemValidation : AbstractValidator<Personagem>
    {
        public PersonagemValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 300).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}