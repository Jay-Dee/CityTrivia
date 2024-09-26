using MediatR;
using CityTrivia.DataAccess.Repositories;

namespace CitiesTrivia.UseCases.Commands
{
    public class DeleteTriviaCommandHandler : IRequestHandler<DeleteTriviaCommand, bool>
    {
        private readonly IUnitOfWork _worker;

        public DeleteTriviaCommandHandler(IUnitOfWork worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }


        public Task<bool> Handle(DeleteTriviaCommand request, CancellationToken cancellationToken)
        {
            _worker.TriviasRepository.Remove(request.TriviaToDelete);
            return _worker.CompleteWork();
        }
    }
}
