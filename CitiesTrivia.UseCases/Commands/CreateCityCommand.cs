using CityTrivia.DataAccess.Entities;
using MediatR;

namespace CitiesTrivia.UseCases.Commands
{
    public class CreateCityCommand  : IRequest<bool>{
        public CreateCityCommand(City cityToAdd) {
            CityToAdd = cityToAdd;
        }

        public City CityToAdd { get; private set; }
    }
}
