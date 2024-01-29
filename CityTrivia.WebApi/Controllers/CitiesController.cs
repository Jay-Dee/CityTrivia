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
        public async Task<ActionResult<bool>> CreateCity(City cityToCreate) {
            await _citiesRepository.AddCity(cityToCreate);
            var cityCreatedSuccesfully = await _citiesRepository.SaveChangesAsync();
            return cityCreatedSuccesfully ? Ok(cityCreatedSuccesfully) : false;
        }
    }   
}
