using CityTrivia.DataAccess.Entities;
using MediatR;
using System.Net;

namespace CitiesTrivia.UseCases.Queries
{
    public class GetCitiesQuery : IRequest<IEnumerable<City>>
    {
        public GetCitiesQuery(string nameToFilter, int toSkip, int toTake)
        {
            NameToFilter = nameToFilter;
            ToSkip = toSkip;
            ToTake = toTake;
        }

        public string? NameToFilter { get; private set; }
        public int ToSkip { get; private set; }
        public int ToTake { get; }
    }
}
