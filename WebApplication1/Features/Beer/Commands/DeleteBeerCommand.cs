using MediatR;

namespace WebApplication1.Features.Beer.Commands
{
    public record DeleteBeerCommand(int id) : IRequest<bool>;
}