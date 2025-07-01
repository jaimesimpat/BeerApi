using System;
using MediatR;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services;

public record BeerRepoValidateUpdateRequest(Func<Beer, bool> filter) : IRequest<bool>;

public class BeerRepoValidateUpdateHandler : IRequestHandler<BeerRepoValidateUpdateRequest, bool>
{
    private readonly IRepository<Beer> _beerRepository;

    public BeerRepoValidateUpdateHandler(IRepository<Beer> beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task<bool> Handle(BeerRepoValidateUpdateRequest request, CancellationToken cancellationToken)
    {
        var existingBeer = _beerRepository.Search(request.filter);
        return existingBeer.Any();
    }
}
