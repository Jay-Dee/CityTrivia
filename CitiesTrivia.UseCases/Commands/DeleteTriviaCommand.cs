using CityTrivia.DataAccess.Entities;
using MediatR;

namespace CitiesTrivia.UseCases.Commands
{
    public class DeleteTriviaCommand  : IRequest<bool>{
        public DeleteTriviaCommand(Trivia triviaToDelete) {
            TriviaToDelete = triviaToDelete;
        }

        public Trivia TriviaToDelete { get; private set; }
    }
}
