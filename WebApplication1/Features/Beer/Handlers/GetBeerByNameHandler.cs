using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Features.Beer.Queries;
using WebApplication1.Services;

namespace WebApplication1.Features.Beer.Handlers
{
    public class GetBeerByNameHandler : IRequestHandler<GetBeerByNameQuery, BeerDto>
    {
        private readonly ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> _beerService;

        public GetBeerByNameHandler([FromKeyedServices("beerService")] ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> beerService)
        {
            _beerService = beerService;
        }

        public async Task<BeerDto> Handle(GetBeerByNameQuery request, CancellationToken cancellationToken)
        {
            return await _beerService.GetByName(request.name);
        }
    }
}