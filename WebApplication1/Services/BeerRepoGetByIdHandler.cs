using System;
using MediatR;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services;

public record BeerRepoGetByIdRequest(int Id) : IRequest<Beer>;

public class BeerRepoGetByIdHandler : IRequestHandler<BeerRepoGetByIdRequest, Beer>
{
    private readonly IRepository<Beer> _beerRepository;

    public BeerRepoGetByIdHandler(IRepository<Beer> beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task<Beer> Handle(BeerRepoGetByIdRequest request, CancellationToken cancellationToken)
    {
        var beer = await _beerRepository.GetById(request.Id);
        return beer;
    }
}