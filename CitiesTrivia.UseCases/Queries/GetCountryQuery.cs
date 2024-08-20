using CityTrivia.DataAccess.Entities;
using MediatR;

namespace CitiesTrivia.UseCases.Queries
{
    public class GetCountryQuery : IRequest<Country>
    {
        public GetCountryQuery(int countryId)
        {
            CountryId = countryId;
        }

        public int CountryId { get; private set; }
    }
}
