using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services;

public record BeerRepoGetByNameRequest(string name) : IRequest<Beer?>;

public class BeerRepoGetByNameHandler : IRequestHandler<BeerRepoGetByNameRequest, Beer?>
{
    private readonly IRepository<Beer> _beerRepository;

    public BeerRepoGetByNameHandler(IRepository<Beer> beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public Task<Beer?> Handle(BeerRepoGetByNameRequest request, CancellationToken cancellationToken)
    {
        var beer = _beerRepository.Search(b => b.Name.Contains(request.name)).FirstOrDefault();
        return Task.FromResult(beer);
    }
}
