using MediatR;
using WebApplication1.DTOs;

namespace WebApplication1.CQRS.Beer.Commands
{
    public record UpdateBeerCommand(int id, BeerUpdateDto beerToUpdate) : IRequest<BeerDto>;
}