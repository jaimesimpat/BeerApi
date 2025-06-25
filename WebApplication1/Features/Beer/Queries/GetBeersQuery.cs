using MediatR;
using WebApplication1.DTOs;

namespace WebApplication1.Features.Beer.Queries
{
    public class GetBeersQuery : IRequest<IEnumerable<BeerDto>>
    {
        // No parameters needed for getting all beers
    }
}