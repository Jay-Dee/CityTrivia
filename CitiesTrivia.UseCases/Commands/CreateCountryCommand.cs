using CityTrivia.DataAccess.Entities;
using MediatR;

namespace CitiesTrivia.UseCases.Commands
{
    public class CreateCountryCommand  : IRequest<bool>{
        public CreateCountryCommand(Country countryToAdd) {
            CountryToAdd = countryToAdd;
        }

        public Country CountryToAdd { get; private set; }
    }
}
