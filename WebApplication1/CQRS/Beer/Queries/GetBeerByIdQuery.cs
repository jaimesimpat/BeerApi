using MediatR;
using WebApplication1.DTOs;

namespace WebApplication1.CQRS.Beer.Queries
{
    public record GetBeerByIdQuery(int id) : IRequest<BeerDto>;
}