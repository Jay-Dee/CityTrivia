using MediatR;
using CityTrivia.DataAccess.Repositories;

namespace CitiesTrivia.UseCases.Commands
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, bool>
    {
        private readonly IUnitOfWork _worker;

        public UpdateCityCommandHandler(IUnitOfWork worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }

        public Task<bool> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            _worker.CitiesRepository.Update(request.CityToUpdate);
            return _worker.CompleteWork();
        }
    }
}
