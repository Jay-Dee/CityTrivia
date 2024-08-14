using CityTrivia.DataAccess.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesTrivia.UseCases.Commands
{
    public class DeleteCityCommand  : IRequest<bool>{
        public DeleteCityCommand(City cityToDelete) {
            CityToDelete = cityToDelete;
        }

        public City CityToDelete { get; private set; }
    }
}
