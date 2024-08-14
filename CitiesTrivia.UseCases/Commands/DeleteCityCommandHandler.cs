using MediatR;
using CityTrivia.DataAccess.Repositories;

namespace CitiesTrivia.UseCases.Commands
{
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, bool>
    {
        private readonly IUnitOfWork _worker;

        public DeleteCityCommandHandler(IUnitOfWork worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }


        public Task<bool> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            _worker.CitiesRepository.RemoveCity(request.CityToDelete);
            return _worker.CompleteWork();
        }
    }
}
