using System;
using MediatR;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services;

public record BeerRepoAddRequest(Beer beer) : IRequest<Beer>;
public class BeerRepoAddHandler : IRequestHandler<BeerRepoAddRequest, Beer>
{
    private readonly IRepository<Beer> _beerRepository;

    public BeerRepoAddHandler(IRepository<Beer> beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task<Beer> Handle(BeerRepoAddRequest request, CancellationToken cancellationToken)
    {
        await _beerRepository.Add(request.beer);
        await _beerRepository.Save();
        return request.beer;
    }
}
