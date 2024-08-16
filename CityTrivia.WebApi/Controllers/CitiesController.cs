using Asp.Versioning;
using AutoMapper;
using CityTrivia.DataAccess.Entities;
using CityTrivia.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using CitiesTrivia.UseCases.Queries;
using CitiesTrivia.UseCases.Commands;

namespace CityTrivia.WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("/api/v{version:apiVersion}/cities")]
    public class CitiesController : ControllerBase {
        private const int MaxNumberOfEntitiesAllowed = 10;

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CitiesController(IMapper mapper, IMediator mediator) {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mapper));
        }

        [ApiVersion("1.0")]
        [RequiredScope("Cities.Read.All")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CityGetModel>>> GetCities([FromQuery] string? nameToFilter, int pageNumber = 1, int pageSize = 10) {
            if (pageSize > MaxNumberOfEntitiesAllowed) {
                pageSize = MaxNumberOfEntitiesAllowed;
            }

            var cities = await _mediator.Send(new GetCitiesQuery(nameToFilter??string.Empty, pageSize * (pageNumber - 1), pageSize));
            return Ok(_mapper.Map<IEnumerable<CityGetModel>>(cities));
        }

        [ApiVersion("1.0")]
        [HttpGet("{cityId:int}", Name = nameof(GetCity))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CityGetModel>> GetCity(int cityId) {
            var city = await _mediator.Send(new GetCityQuery(cityId));
            return city != null
                ? Ok(_mapper.Map<CityGetModel>(city))
                : NotFound($"No city exists with Id = {cityId}");
        }

        [ApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> CreateCity([FromBody]CityPostModel cityToCreate) {
            var cityToAdd = _mapper.Map<CityPostModel, City>(cityToCreate);
            var cityCreatedSuccesfully = await _mediator.Send(new CreateCityCommand(cityToAdd));
            return cityCreatedSuccesfully
                ? CreatedAtRoute(nameof(GetCity), new { cityId = cityToAdd.Id }, _mapper.Map<CityGetModel>(cityToAdd))
                : StatusCode(StatusCodes.Status500InternalServerError, "Failed to create city");
        }

        [ApiVersion("1.0")]
        [HttpDelete("{cityId:int}", Name = nameof(DeleteCity))]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCity(int cityId) {
            var cityToDelete = await _mediator.Send(new GetCityQuery(cityId));
            if (cityToDelete != null) {
                var cityDeletedSuccessfully = await _mediator.Send(new DeleteCityCommand(cityToDelete));
                return cityDeletedSuccessfully
                    ? Accepted(nameof(DeleteCity), cityId)
                    : StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete city");
            } else {
                return NotFound($"No city exists with Id = {cityId}");
            }
        }

        [ApiVersion("1.0")]
        [HttpPut("{cityId:int}", Name = nameof(ReplaceCity))]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ReplaceCity(int cityId, CityPostModel cityToReplace) {
            var city = await _mediator.Send(new GetCityQuery(cityId));
            if (city != null) {
                _mapper.Map(cityToReplace, city);
                var cityUpdatedSuccessfully = await _mediator.Send(new UpdateCityCommand(city));
                return cityUpdatedSuccessfully
                    ? Accepted(nameof(ReplaceCity), new { cityId = city.Id })
                    : StatusCode(StatusCodes.Status500InternalServerError, "Failed to update city");
            } else {
                return NotFound($"No city exists with Id = {cityId}");
            }
        }

        [ApiVersion("1.0")]
        [HttpPatch("{cityId:int}", Name = nameof(PatchCity))]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PatchCity(int cityId, [FromBody]JsonPatchDocument<CityPostModel> patchDocument) {
            var city = await _mediator.Send(new GetCityQuery(cityId));
            if (city != null) {
                var cityToPatch = _mapper.Map<CityPostModel>(city);
                patchDocument.ApplyTo(cityToPatch, ModelState);
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                if (!TryValidateModel(cityToPatch)) {
                    return BadRequest(ModelState);
                }

                _mapper.Map(cityToPatch, city);
                var cityPatchedSuccessfully = await _mediator.Send(new UpdateCityCommand(city));
                return cityPatchedSuccessfully
                    ? Accepted(nameof(PatchCity), new { cityId = city.Id })
                    : StatusCode(StatusCodes.Status500InternalServerError, "Failed to update city");
            } else {
                return NotFound($"No city exists with Id = {cityId}");
            }
        }
    }
}
