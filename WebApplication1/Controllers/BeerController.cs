using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.CQRS.Beer.Commands;
using WebApplication1.CQRS.Beer.Queries;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<BeerInsertDto> _beerInsertValidator;
        private readonly IValidator<BeerUpdateDto> _beerUpdateValidator;
        private readonly ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> _beerService;

        public BeerController(
            IMediator mediator,
            IValidator<BeerInsertDto> beerInsertValidator,
            IValidator<BeerUpdateDto> beerUpdateValidator,
            [FromKeyedServices("beerService")] ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> beerService)
        {
            _mediator = mediator;
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _beerService = beerService;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get()
        {
            return await _mediator.Send(new GetBeersQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beerDto = await _mediator.Send(new GetBeerByIdQuery(id));
            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<BeerDto>> GetByName(string name)
        {
            var beerDto = await _mediator.Send(new GetBeerByNameQuery(name));
            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            try
            {
                var beerDto = await _mediator.Send(new CreateBeerCommand(beerInsertDto));
                return CreatedAtAction(nameof(GetById), new { id = beerDto.Id }, beerDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            try
            {
                var beerDto = await _mediator.Send(new UpdateBeerCommand(id, beerUpdateDto));
                
                return Ok(beerDto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await _mediator.Send(new DeleteBeerCommand(id));
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
