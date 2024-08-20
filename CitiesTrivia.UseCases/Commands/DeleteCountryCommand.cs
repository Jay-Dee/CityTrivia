using CityTrivia.DataAccess.Entities;
using MediatR;

namespace CitiesTrivia.UseCases.Commands
{
    public class DeleteCountryCommand  : IRequest<bool>{
        public DeleteCountryCommand(Country countryToDelete) {
            CountryToDelete = countryToDelete;
        }

        public Country CountryToDelete { get; private set; }
    }
}
