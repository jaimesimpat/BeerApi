using MediatR;

namespace WebApplication1.Features.Beer.Commands
{
    public class DeleteBeerCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}