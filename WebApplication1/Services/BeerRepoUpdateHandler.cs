using System;
using MediatR;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services;

public record BeerRepoUpdateRequest(Beer beer) : IRequest<Beer>;
public class BeerRepoUpdateHandler : IRequestHandler<BeerRepoUpdateRequest, Beer>
{
    private readonly IRepository<Beer> _beerRepository;

    public BeerRepoUpdateHandler(IRepository<Beer> beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task<Beer> Handle(BeerRepoUpdateRequest request, CancellationToken cancellationToken)
    {
        _beerRepository.Update(request.beer);
        await _beerRepository.Save();

        return request.beer;
    }
}
