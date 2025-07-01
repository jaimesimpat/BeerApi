using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.CQRS.Beer.Queries;
using WebApplication1.Services;

namespace WebApplication1.CQRS.Beer.Handlers
{
    public class GetBeersHandler : IRequestHandler<GetBeersQuery, IEnumerable<BeerDto>>
    {
        private readonly ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> _beerService;

        public GetBeersHandler([FromKeyedServices("beerService")] ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> beerService)
        {
            _beerService = beerService;
        }

        public async Task<IEnumerable<BeerDto>> Handle(GetBeersQuery request, CancellationToken cancellationToken)
        {
            return await _beerService.Get();
        }
    }
}