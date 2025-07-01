using System;
using MediatR;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services;

public record BeerRepoGetRequest() : IRequest<IEnumerable<Beer>>;

public class BeerRepoGetHandler : IRequestHandler<BeerRepoGetRequest, IEnumerable<Beer>>
{
    private readonly IRepository<Beer> _beerRepository;

    public BeerRepoGetHandler(IRepository<Beer> beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task<IEnumerable<Beer>> Handle(BeerRepoGetRequest request, CancellationToken cancellationToken)
    {
        var beers = await _beerRepository.Get();
        return beers;
    }
}
