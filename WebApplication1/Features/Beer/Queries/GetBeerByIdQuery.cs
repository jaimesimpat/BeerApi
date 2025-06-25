using MediatR;
using WebApplication1.DTOs;

namespace WebApplication1.Features.Beer.Queries
{
    public class GetBeerByIdQuery : IRequest<BeerDto>
    {
        public int Id { get; set; }
    }
}