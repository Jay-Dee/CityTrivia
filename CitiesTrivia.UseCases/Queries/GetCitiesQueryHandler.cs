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
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, IEnumerable<City>>
    {
        private readonly IUnitOfWork _worker;

        public GetCitiesQueryHandler(IUnitOfWork worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }

        public async Task<IEnumerable<City>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            return await _worker.CitiesRepository.GetCitiesAsync(request.NameToFilter, request.ToSkip, request.ToTake);
        }
    }
}
