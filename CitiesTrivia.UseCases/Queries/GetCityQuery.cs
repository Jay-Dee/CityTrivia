using CityTrivia.DataAccess.Entities;
using MediatR;
using System.Net;

namespace CitiesTrivia.UseCases.Queries
{
    public class GetCityQuery : IRequest<City>
    {
        public GetCityQuery(int cityId)
        {
            CityId = cityId;
        }

        public int CityId { get; private set; }
    }
}
