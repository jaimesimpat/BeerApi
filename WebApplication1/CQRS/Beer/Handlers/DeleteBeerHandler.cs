using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.CQRS.Beer.Commands;
using WebApplication1.Services;

namespace WebApplication1.CQRS.Beer.Handlers
{
    public class DeleteBeerHandler : IRequestHandler<DeleteBeerCommand, bool>
    {
        private readonly ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> _beerService;

        public DeleteBeerHandler(
            [FromKeyedServices("beerService")] ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> beerService)
        {
            _beerService = beerService;
        }

        public async Task<bool> Handle(DeleteBeerCommand request, CancellationToken cancellationToken)
        {
            var removed = await _beerService.Remove(request.id);
            
            if (!removed)
                throw new KeyNotFoundException($"Beer with ID {request.id} not found");
                
            return true;
        }
    }
}