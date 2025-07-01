using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Features.Beer.Commands;
using WebApplication1.Services;

namespace WebApplication1.Features.Beer.Handlers
{
    public class CreateBeerHandler : IRequestHandler<CreateBeerCommand, BeerDto>
    {
        private readonly ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> _beerService;
        private readonly IValidator<BeerInsertDto> _validator;

        public CreateBeerHandler(
            [FromKeyedServices("beerService")] ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> beerService,
            IValidator<BeerInsertDto> validator)
        {
            _beerService = beerService;
            _validator = validator;
        }

        public async Task<BeerDto> Handle(CreateBeerCommand request, CancellationToken cancellationToken)
        {
            // Validate using FluentValidation
            var validationResult = await _validator.ValidateAsync(request.beerInsertDto, cancellationToken);
            
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            // Validate using business rules
            if (!_beerService.Validate(request.beerInsertDto))
                throw new ValidationException(_beerService.Errors.Select(e =>
                    new ValidationFailure(string.Empty, e)).ToList());

            // Add the beer
            return await _beerService.Add(request.beerInsertDto);
        }
    }
}