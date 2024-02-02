using AutoMapper;
using CityTrivia.WebApi.Entities;
using CityTrivia.WebApi.Models;
using CityTrivia.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityTrivia.WebApi.Controllers {
    [ApiController]
    [Route("/api/cities")]
    public class CitiesController : ControllerBase {
        private readonly ICitiesRepository _citiesRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICitiesRepository citiesRepository, IMapper mapper) {
            _citiesRepository = citiesRepository ?? throw new ArgumentNullException(nameof(citiesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CityGetModel>>> GetCities() {
            var cities = await _citiesRepository.GetCitiesAsync();
            return Ok(_mapper.Map<IEnumerable<CityGetModel>>(cities));
        }

        [HttpGet("{cityId:int}", Name = nameof(GetCity))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CityGetModel>> GetCity(int cityId) {
            var city = await _citiesRepository.GetCityAsync(cityId);
            return city == null ? NotFound() : Ok(_mapper.Map<CityGetModel>(city));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> CreateCity(CityPostModel cityToCreate) {
            var cityToAdd = _mapper.Map<CityPostModel, City>(cityToCreate);
            _citiesRepository.AddCity(cityToAdd);
            var cityCreatedSuccesfully = await _citiesRepository.SaveChangesAsync();
            return cityCreatedSuccesfully ? Ok(cityCreatedSuccesfully) : BadRequest();
        }

        [HttpDelete("{cityId:int}", Name = nameof(DeleteCity))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCity(int cityId) {
            var cityToDelete = await _citiesRepository.GetCityAsync(cityId);
            if(cityToDelete != null) {
                _citiesRepository.RemoveCity(cityToDelete);
                var cityDeleted = await _citiesRepository.SaveChangesAsync();
                return cityDeleted ? Ok(cityDeleted) : BadRequest();
            } else {
                return NotFound();
            }
        }

        [HttpPut("{cityId:int}", Name =nameof(UpdateCity))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCity(int cityId, CityPostModel cityToUpdate) {
            var city = await _citiesRepository.GetCityAsync(cityId);
            if (city != null) {
                _citiesRepository.UpdateCity(_mapper.Map<CityPostModel, City>(cityToUpdate, city));
                var cityUpdatedSuccessfully = await _citiesRepository.SaveChangesAsync();
                return cityUpdatedSuccessfully ? Ok(cityUpdatedSuccessfully) : BadRequest();
            } else {
                return NotFound();
            }
        }
    }   
}
