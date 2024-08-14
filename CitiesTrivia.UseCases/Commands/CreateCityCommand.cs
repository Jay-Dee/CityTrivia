using CityTrivia.DataAccess.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesTrivia.UseCases.Commands
{
    public class CreateCityCommand  : IRequest<bool>{
        public CreateCityCommand(City cityToAdd) {
            CityToAdd = cityToAdd;
        }

        public City CityToAdd { get; private set; }
    }
}
