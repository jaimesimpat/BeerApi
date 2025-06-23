using FluentValidation;
using WebApplication1.DTOs;

namespace WebApplication1.Validators
{
    public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
    {
        public BeerInsertValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is must");
        }
    }
}
