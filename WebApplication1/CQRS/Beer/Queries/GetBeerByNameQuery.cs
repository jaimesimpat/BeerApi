using MediatR;
using WebApplication1.DTOs;

namespace WebApplication1.CQRS.Beer.Queries
{
    public record GetBeerByNameQuery(string name) : IRequest<BeerDto>;
}