﻿using CityTrivia.DataAccess.Entities;
using MediatR;

namespace CitiesTrivia.UseCases.Queries
{
    public class GetTriviasQuery : IRequest<IEnumerable<Trivia>>
    {
        public GetTriviasQuery(string nameToFilter, int toSkip, int toTake)
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
