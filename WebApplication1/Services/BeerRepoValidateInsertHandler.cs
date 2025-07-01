using System;
using MediatR;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services;

public record BeerRepoValidateInsertRequest(Func<Beer, bool> filter) : IRequest<bool>;
public class BeerRepoValidateInsertHandler : IRequestHandler<BeerRepoValidateInsertRequest, bool>
{
    private readonly IRepository<Beer> _beerRepository;

    public BeerRepoValidateInsertHandler(IRepository<Beer> beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task<bool> Handle(BeerRepoValidateInsertRequest request, CancellationToken cancellationToken)
    {
        var existingBeer = _beerRepository.Search(request.filter);
        return existingBeer.Any();
    }
}