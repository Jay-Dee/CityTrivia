using MediatR;
using CityTrivia.DataAccess.Entities;
using CityTrivia.DataAccess.Repositories;

namespace CitiesTrivia.UseCases.Queries
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, IEnumerable<Country>>
    {
        private readonly IUnitOfWork _worker;

        public GetCountriesQueryHandler(IUnitOfWork worker)
        {
            _worker = worker;
        }

        public async Task<IEnumerable<Country>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            return await _worker.CountriesRepository.GetAllAsync(request.NameToFilter, request.ToSkip, request.ToTake);
        }
    }
}
