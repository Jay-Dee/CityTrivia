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
        public async Task<ActionResult<IEnumerable<City>>> GetCities() {
            return Ok(await _citiesRepository.GetCitiesAsync());
        }
    }
}
