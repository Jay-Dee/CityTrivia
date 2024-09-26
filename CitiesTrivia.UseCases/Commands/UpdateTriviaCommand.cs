using CityTrivia.DataAccess.Entities;
using MediatR;

namespace CitiesTrivia.UseCases.Commands
{
    public class UpdateTriviaCommand  : IRequest<bool>{
        public UpdateTriviaCommand(Trivia triviaToUpdate) {
            TriviaToUpdate = triviaToUpdate;
        }

        public Trivia TriviaToUpdate { get; private set; }
    }
}
