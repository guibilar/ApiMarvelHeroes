using FluentValidation;

namespace MarvelHeroes.Business.Models.Validations
{
    public class HeroValidation : AbstractValidator<Hero>
    {
        public HeroValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 300).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}