using CityTrivia.WebApi.Entities;
using CityTrivia.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityTrivia.WebApi.Controllers {
    [ApiController]
    [Route("/api/cities")]
    public class CitiesController : ControllerBase {
        private readonly ICitiesRepository _citiesRepository;

        public CitiesController(ICitiesRepository citiesRepository) {
            _citiesRepository = citiesRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<City>>> GetCities() {
            return Ok(await _citiesRepository.GetCitiesAsync());
        }

        [HttpGet("{cityId:int}", Name = nameof(GetCity))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<City>> GetCity(int cityId) {
            var city = await _citiesRepository.GetCity(cityId);
            return city == null ? NotFound() : Ok(city);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> CreateCity(City cityToCreate) {
            _citiesRepository.AddCity(cityToCreate);
            var cityCreatedSuccesfully = await _citiesRepository.SaveChangesAsync();
            return cityCreatedSuccesfully ? Ok() : BadRequest();
        }

        [HttpDelete("{cityId:int}", Name = nameof(DeleteCity))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCity(int cityId) {
            var cityToDelete = await _citiesRepository.GetCity(cityId);
            if(cityToDelete != null) {
                _citiesRepository.RemoveCity(cityToDelete);
                var cityDeleted = await _citiesRepository.SaveChangesAsync();
                return cityDeleted ? Ok() : BadRequest();
            } else {
                return NotFound();
            }
        }

        [HttpPut("{cityId:int}", Name =nameof(UpdateCity))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCity(int cityId, City city) {
            var cityToUpdate = await _citiesRepository.GetCity(cityId);
            if(cityToUpdate != null) {
                cityToUpdate.Name = city.Name;
                cityToUpdate.Description = city.Description;
                _citiesRepository.UpdateCity(cityToUpdate);
                var cityUpdatedSuccessfully = await _citiesRepository.SaveChangesAsync();
                return cityUpdatedSuccessfully ? Ok() : BadRequest();
            } else {
                return NotFound();
            }
        }
    }   
}
