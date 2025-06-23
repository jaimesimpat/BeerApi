using FluentValidation;
using WebApplication1.DTOs;

namespace WebApplication1.Validators
{
    public class BeerUpdateValidator : AbstractValidator<BeerUpdateDto>
    {
        public BeerUpdateValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El id es obligatorio");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.BrandID).NotEmpty().WithMessage("La marca es obligatoria");
            RuleFor(x => x.Alcohol).NotEmpty().WithMessage("El alcohol es obligatorio");
        }
    }
} 