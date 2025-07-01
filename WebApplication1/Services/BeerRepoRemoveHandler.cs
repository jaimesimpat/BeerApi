using System;
using MediatR;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services;

public record BeerRepoRemoveRequest(int id) : IRequest<bool>;

public class BeerRepoRemoveHandler : IRequestHandler<BeerRepoRemoveRequest, bool>
{
    private readonly IRepository<Beer> _beerRepository;

    public BeerRepoRemoveHandler(IRepository<Beer> beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task<bool> Handle(BeerRepoRemoveRequest request, CancellationToken cancellationToken)
    {
        var beer = await _beerRepository.GetById(request.id);
        if (beer == null) return false;

        await _beerRepository.Remove(request.id);
        await _beerRepository.Save();

        return true;
    }
}