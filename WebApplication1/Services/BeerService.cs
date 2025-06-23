using AutoMapper;
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
        private IRepository<Beer> _beerRepository;
        private IMapper _mapper;

        public List<string> Errors { get; }

        public BeerService(
            IMapper maper,
            IRepository<Beer> beerRepository)
        {
            _beerRepository = beerRepository;
            _mapper = maper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
            var beers = await _beerRepository.Get();
            return beers.Select(b => _mapper.Map<BeerDto>(b));
        }

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

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

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            var beerDto = _mapper.Map<BeerDto>(beer);

            return beerDto;
        }

        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                _mapper.Map(beerUpdateDto, beer);
                _beerRepository.Update(beer);
                await _beerRepository.Save();

                return _mapper.Map<BeerDto>(beer);
            }

            return null;
        }

        public bool Validate(BeerInsertDto beerInsertDto)
        {
            if (_beerRepository.Search(b => b.Name == beerInsertDto.Name).Count() > 0)
            {
                Errors.Add("No puede existir una cerveza con un nombre ya existente");
                return false;
            }

            return true;
        }

        public bool Validate(BeerUpdateDto beerUpdateDto)
        {
            if (_beerRepository.Search(b => b.Name == beerUpdateDto.Name && b.BeerID != beerUpdateDto.Id).Count() > 0)
            {
                Errors.Add("No puede existir una cerveza con un nombre ya existente");
                return false;
            }

            return true;
        }

        public async Task<bool> Remove(int id)
        {
            var beer = await _beerRepository.GetById(id);
            if (beer == null)
                return false;
            await _beerRepository.Remove(id);
            await _beerRepository.Save();
            return true;
        }

        public async Task<BeerDto> GetByName(string name)
        {
            var beer = _beerRepository.Search(b => b.Name.Contains(name)).FirstOrDefault();

            if (beer != null)
            {
                return _mapper.Map<BeerDto>(beer);
            }

            return null;
        }
    }
}
