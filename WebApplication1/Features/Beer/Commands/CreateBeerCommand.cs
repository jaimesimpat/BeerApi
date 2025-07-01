using MediatR;
using WebApplication1.DTOs;

namespace WebApplication1.Features.Beer.Commands
{
    public record CreateBeerCommand(BeerInsertDto beerInsertDto) : IRequest<BeerDto>;

}