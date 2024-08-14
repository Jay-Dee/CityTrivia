using CityTrivia.DataAccess.Entities;
using MediatR;

namespace CitiesTrivia.UseCases.Commands
{
    public class DeleteCityCommand  : IRequest<bool>{
        public DeleteCityCommand(City cityToDelete) {
            CityToDelete = cityToDelete;
        }

        public City CityToDelete { get; private set; }
    }
}
