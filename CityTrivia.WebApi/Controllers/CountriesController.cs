using Asp.Versioning;
using AutoMapper;
using CityTrivia.DataAccess.Entities;
using CityTrivia.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using CitiesTrivia.UseCases.Queries;
using CitiesTrivia.UseCases.Commands;

namespace CountryTrivia.WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("/api/v{version:apiVersion}/Countries")]
    public class CountriesController : ControllerBase {
        private const int MaxNumberOfEntitiesAllowed = 10;

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CountriesController(IMapper mapper, IMediator mediator) {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mapper));
        }

        [ApiVersion("1.0")]
        [RequiredScope("Countries.Read.All")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CountryGetModel>>> GetCountries([FromQuery] string? nameToFilter, int pageNumber = 1, int pageSize = 10) {
            if (pageSize > MaxNumberOfEntitiesAllowed) {
                pageSize = MaxNumberOfEntitiesAllowed;
            }

            var Countries = await _mediator.Send(new GetCountriesQuery(nameToFilter??string.Empty, pageSize * (pageNumber - 1), pageSize));
            return Ok(_mapper.Map<IEnumerable<CountryGetModel>>(Countries));
        }

        [ApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> CreateCountry([FromBody]CountryPostModel CountryToCreate) {
            var CountryToAdd = _mapper.Map<CountryPostModel, Country>(CountryToCreate);
            var CountryCreatedSuccesfully = await _mediator.Send(new CreateCountryCommand(CountryToAdd));
            return CountryCreatedSuccesfully
                ? CreatedAtRoute(nameof(CreateCountry), new { CountryId = CountryToAdd.Id }, _mapper.Map<CountryGetModel>(CountryToAdd))
                : StatusCode(StatusCodes.Status500InternalServerError, "Failed to create Country");
        }

        [ApiVersion("1.0")]
        [HttpDelete("{CountryId:int}", Name = nameof(DeleteCountry))]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCountry(int CountryId) {
            var CountryToDelete = await _mediator.Send(new GetCountryQuery(CountryId));
            if (CountryToDelete != null) {
                var CountryDeletedSuccessfully = await _mediator.Send(new DeleteCountryCommand(CountryToDelete));
                return CountryDeletedSuccessfully
                    ? Accepted(nameof(DeleteCountry), CountryId)
                    : StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete Country");
            } else {
                return NotFound($"No Country exists with Id = {CountryId}");
            }
        }
    }
}
