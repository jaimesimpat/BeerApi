using MediatR;
using WebApplication1.DTOs;

namespace WebApplication1.Features.Beer.Queries
{
    public class GetBeerByNameQuery : IRequest<BeerDto>
    {
        public string Name { get; set; }
    }
}