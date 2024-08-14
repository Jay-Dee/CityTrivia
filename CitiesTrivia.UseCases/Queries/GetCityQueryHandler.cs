using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTrivia.DataAccess.Entities;
using CityTrivia.DataAccess.Repositories;

namespace CitiesTrivia.UseCases.Queries
{
    public class GetCityQueryHandler : IRequestHandler<GetCityQuery, City>
    {
        private readonly IUnitOfWork _worker;

        public GetCityQueryHandler(IUnitOfWork worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }

        public async Task<City> Handle(GetCityQuery request, CancellationToken cancellationToken)
        {
            return await _worker.CitiesRepository.GetCityAsync(request.CityId);
        }
    }
}
