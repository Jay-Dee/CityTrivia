using CityTrivia.DataAccess.Entities;
using MediatR;

namespace CitiesTrivia.UseCases.Commands
{
    public class UpdateCityCommand  : IRequest<bool>{
        public UpdateCityCommand(City cityToDelete) {
            CityToUpdate = cityToDelete;
        }

        public City CityToUpdate { get; private set; }
    }
}
