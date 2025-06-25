using MediatR;
using WebApplication1.DTOs;

namespace WebApplication1.Features.Beer.Commands
{
    public class CreateBeerCommand : IRequest<BeerDto>
    {
        public BeerInsertDto BeerToCreate { get; set; }
    }
}