using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    public interface ICommonService<T, TI, TU>
    {
        public List<string> Errors { get; }
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI beerInsertDto);
        Task<T> Update(int id, TU dto);
        bool Validate(TI dto);

        bool Validate(TU dto);

        Task<bool> Remove(int id);
        Task<T> GetByName(string name);
    }

    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        private IMapper _mapper;
        private IMediator _mediator;

        public List<string> Errors { get; }

        public BeerService(
            IMapper maper,
            IMediator mediator)
        {
            _mapper = maper;
            _mediator = mediator;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
            var beers = await _mediator.Send(new BeerRepoGetRequest());
            return beers.Select(b => _mapper.Map<BeerDto>(b));
        }

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _mediator.Send(new BeerRepoGetByIdRequest(id));

            if (beer != null)
            {
                var beerDto = _mapper.Map<BeerDto>(beer);
                return beerDto;
            }

            return null;
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            var beer = _mapper.Map<Beer>(beerInsertDto);
            if (!Validate(beerInsertDto))
            {
                throw new ValidationException(Errors.First());
            }

            await _mediator.Send(new BeerRepoAddRequest(beer));

            var beerDto = _mapper.Map<BeerDto>(beer);

            return beerDto;
        }

        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _mediator.Send(new BeerRepoGetByIdRequest(id));

            if (beer != null)
            {
                _mapper.Map(beerUpdateDto, beer);
                await _mediator.Send(new BeerRepoUpdateRequest(beer));

                return _mapper.Map<BeerDto>(beer);
            }

            return null;
        }

        public bool Validate(BeerInsertDto beerInsertDto)
        {
            if (_mediator.Send(new BeerRepoValidateInsertRequest(b => b.Name == beerInsertDto.Name)).Result)
            {
                Errors.Add("No puede existir una cerveza con un nombre ya existente");
                return false;
            }

            return true;
        }

        public bool Validate(BeerUpdateDto beerUpdateDto)
        {
            if (_mediator.Send(new BeerRepoValidateInsertRequest(b => b.Name == beerUpdateDto.Name && b.Id != beerUpdateDto.Id)).Result)
            {
                Errors.Add("No puede existir una cerveza con un nombre ya existente");
                return false;
            }

            return true;
        }

        public async Task<bool> Remove(int id)
        {
            var beer = await _mediator.Send(new BeerRepoGetByIdRequest(id));
            if (beer == null)
                return false;
            await _mediator.Send(new BeerRepoRemoveRequest(id));
            return true;
        }

        public async Task<BeerDto> GetByName(string name)
        {
            var beer = await _mediator.Send(new BeerRepoGetByNameRequest(name));

            if (beer != null)
            {
                return _mapper.Map<BeerDto>(beer);
            }

            return null;
        }
    }
}
