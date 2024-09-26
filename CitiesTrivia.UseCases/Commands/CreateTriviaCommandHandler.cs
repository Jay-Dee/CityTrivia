using CityTrivia.DataAccess.Repositories;
using MediatR;

namespace CitiesTrivia.UseCases.Commands
{
    public class CreateTriviaCommandHandler : IRequestHandler<CreateTriviaCommand, bool>
    {
        private readonly IUnitOfWork _worker;

        public CreateTriviaCommandHandler(IUnitOfWork worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }

        public Task<bool> Handle(CreateTriviaCommand request, CancellationToken cancellationToken)
        {
            _worker.TriviasRepository.Add(request.TriviaToAdd);
            return _worker.CompleteWork();
        }
    }
}
