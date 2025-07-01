using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.CQRS.Beer.Commands;
using WebApplication1.Services;

namespace WebApplication1.CQRS.Beer.Handlers
{
    public class UpdateBeerHandler : IRequestHandler<UpdateBeerCommand, BeerDto>
    {
        private readonly ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> _beerService;
        private readonly IValidator<BeerUpdateDto> _validator;

        public UpdateBeerHandler(
            [FromKeyedServices("beerService")] ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> beerService,
            IValidator<BeerUpdateDto> validator)
        {
            _beerService = beerService;
            _validator = validator;
        }

        public async Task<BeerDto> Handle(UpdateBeerCommand request, CancellationToken cancellationToken)
        {
            // Validate using FluentValidation
            var validationResult = await _validator.ValidateAsync(request.beerToUpdate, cancellationToken);
            
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            // Validate using business rules
            if (!_beerService.Validate(request.beerToUpdate))
                throw new ValidationException(_beerService.Errors.Select(e => 
                    new ValidationFailure(string.Empty, e)).ToList());

            // Update the beer
            var beerDto = await _beerService.Update(request.id, request.beerToUpdate);
            
            if (beerDto == null)
                throw new KeyNotFoundException($"Beer with ID {request.id} not found");
                
            return beerDto;
        }
    }
}