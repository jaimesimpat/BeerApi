using MediatR;
using WebApplication1.DTOs;

namespace WebApplication1.Features.Beer.Commands
{
    public class UpdateBeerCommand : IRequest<BeerDto>
    {
        public int Id { get; set; }
        public BeerUpdateDto BeerToUpdate { get; set; }
    }
}