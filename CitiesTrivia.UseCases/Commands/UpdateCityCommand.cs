using CityTrivia.DataAccess.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesTrivia.UseCases.Commands
{
    public class UpdateCityCommand  : IRequest<bool>{
        public UpdateCityCommand(City cityToDelete) {
            CityToUpdate = cityToDelete;
        }

        public City CityToUpdate { get; private set; }
    }
}
