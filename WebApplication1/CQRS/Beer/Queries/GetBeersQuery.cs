using MediatR;
using WebApplication1.DTOs;

namespace WebApplication1.CQRS.Beer.Queries
{
    public record GetBeersQuery : IRequest<IEnumerable<BeerDto>>;
}