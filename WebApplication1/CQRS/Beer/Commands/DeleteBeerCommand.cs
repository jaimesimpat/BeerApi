using MediatR;

namespace WebApplication1.CQRS.Beer.Commands
{
    public record DeleteBeerCommand(int id) : IRequest<bool>;
}