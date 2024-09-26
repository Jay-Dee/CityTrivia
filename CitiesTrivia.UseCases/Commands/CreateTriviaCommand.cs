using CityTrivia.DataAccess.Entities;
using MediatR;

namespace CitiesTrivia.UseCases.Commands
{
    public class CreateTriviaCommand : IRequest<bool>
    {
        public CreateTriviaCommand(Trivia triviaToAdd) {
            TriviaToAdd = triviaToAdd;
        }

        public Trivia TriviaToAdd { get; private set; }
    }
}
