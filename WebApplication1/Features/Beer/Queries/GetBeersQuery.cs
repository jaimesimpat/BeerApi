using MediatR;
using WebApplication1.DTOs;

namespace WebApplication1.Features.Beer.Queries
{
    public record GetBeersQuery : IRequest<IEnumerable<BeerDto>>;
}