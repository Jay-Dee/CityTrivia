using Asp.Versioning;
using AutoMapper;
using CityTrivia.DataAccess.Entities;
using CityTrivia.DataAccess.Repositories;
using CityTrivia.WebApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace CityTrivia.WebApi.Controllers {
    //[Authorize]
    [ApiController]
    [Route("/api/v{version:apiVersion}/cities")]
    public class CitiesController : ControllerBase {
        private const int MaxNumberOfEntitiesAllowed = 10;

        private readonly IUnitOfWork _worker;
        private readonly IMapper _mapper;

        public CitiesController(IUnitOfWork citiesRepository, IMapper mapper) {
            _worker = citiesRepository ?? throw new ArgumentNullException(nameof(citiesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [ApiVersion("1.0")]
        [ApiVersion("2.0")]
        [RequiredScope("Cities.Read.All")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CityGetModel>>> GetCities([FromQuery] string? nameToFilter, int pageNumber = 1, int pageSize = 10) {
            if (pageSize > MaxNumberOfEntitiesAllowed) {
                pageSize = MaxNumberOfEntitiesAllowed;
            }
            var cities = await _worker.CitiesRepository.GetCitiesAsync(nameToFilter, pageSize * (pageNumber - 1), pageSize);
            return Ok(_mapper.Map<IEnumerable<CityGetModel>>(cities));
        }

        [ApiVersion("2.0")]
        [HttpGet("{cityId:int}", Name = nameof(GetCity))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CityGetModel>> GetCity(int cityId) {
            var city = await _worker.CitiesRepository.GetCityAsync(cityId);
            return city != null
                ? Ok(_mapper.Map<CityGetModel>(city))
                : NotFound($"No city exists with Id = {cityId}");
        }

        [ApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> CreateCity(CityPostModel cityToCreate) {
            var cityToAdd = _mapper.Map<CityPostModel, City>(cityToCreate);
            _worker.CitiesRepository.AddCity(cityToAdd);
            var cityCreatedSuccesfully = await _worker.CompleteWork();
            return cityCreatedSuccesfully
                ? CreatedAtRoute(nameof(GetCity), new { cityId = cityToAdd.Id }, _mapper.Map<CityGetModel>(cityToAdd))
                : StatusCode(StatusCodes.Status500InternalServerError, "Failed to create city");
        }

        [ApiVersion("1.0")]
        [HttpDelete("{cityId:int}", Name = nameof(DeleteCity))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCity(int cityId) {
            var cityToDelete = await _worker.CitiesRepository.GetCityAsync(cityId);
            if (cityToDelete != null) {
                _worker.CitiesRepository.RemoveCity(cityToDelete);
                var cityDeleted = await _worker.CompleteWork();
                return cityDeleted
                    ? NoContent()
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
        public async Task<ActionResult> ReplaceCity(int cityId, CityPostModel cityToUpdate) {
            var city = await _worker.CitiesRepository.GetCityAsync(cityId);
            if (city != null) {
                _worker.CitiesRepository.UpdateCity(_mapper.Map(cityToUpdate, city));
                var cityUpdatedSuccessfully = await _worker.CompleteWork();
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
            var city = await _worker.CitiesRepository.GetCityAsync(cityId);
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
                await _worker.CompleteWork();
                return Accepted(nameof(PatchCity), new {cityId = city.Id});
            } else {
                return NotFound($"No city exists with Id = {cityId}");
            }
        }
    }
}
