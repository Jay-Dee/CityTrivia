using MediatR;
using CityTrivia.DataAccess.Entities;
using CityTrivia.DataAccess.Repositories;

namespace CitiesTrivia.UseCases.Queries
{
    public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, Country>
    {
        private readonly IUnitOfWork _worker;

        public GetCountryQueryHandler(IUnitOfWork worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }

        public async Task<Country> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            return await _worker.CountriesRepository.GetAsync(request.CountryId);
        }
    }
}
