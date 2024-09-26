using CityTrivia.DataAccess.Entities;
using MediatR;

namespace CitiesTrivia.UseCases.Queries
{
    public class GetTriviaQuery : IRequest<Trivia>
    {
        public GetTriviaQuery(int triviaId)
        {
            TriviaId = triviaId;
        }

        public int TriviaId { get; }
    }
}
