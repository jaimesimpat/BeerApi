using MediatR;
using WebApplication1.DTOs;

namespace WebApplication1.Features.Beer.Commands
{
    public record UpdateBeerCommand(int id, BeerUpdateDto beerToUpdate) : IRequest<BeerDto>;
}