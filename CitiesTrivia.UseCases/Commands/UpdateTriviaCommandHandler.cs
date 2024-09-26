using MediatR;
using CityTrivia.DataAccess.Repositories;

namespace CitiesTrivia.UseCases.Commands
{
    public class UpdateTriviaCommandHandler : IRequestHandler<UpdateTriviaCommand, bool>
    {
        private readonly IUnitOfWork _worker;

        public UpdateTriviaCommandHandler(IUnitOfWork worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }

        public Task<bool> Handle(UpdateTriviaCommand request, CancellationToken cancellationToken)
        {
            _worker.TriviasRepository.Update(request.TriviaToUpdate);
            return _worker.CompleteWork();
        }
    }
}
