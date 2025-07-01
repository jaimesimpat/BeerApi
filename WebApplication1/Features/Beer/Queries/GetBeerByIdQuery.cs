using MediatR;
using WebApplication1.DTOs;

namespace WebApplication1.Features.Beer.Queries
{
    public record GetBeerByIdQuery(int id) : IRequest<BeerDto>;
}