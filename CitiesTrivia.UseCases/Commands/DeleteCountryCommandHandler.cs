using MediatR;
using CityTrivia.DataAccess.Repositories;

namespace CitiesTrivia.UseCases.Commands
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, bool>
    {
        private readonly IUnitOfWork _worker;

        public DeleteCountryCommandHandler(IUnitOfWork worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }


        public Task<bool> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            _worker.CountriesRepository.Remove(request.CountryToDelete);
            return _worker.CompleteWork();
        }
    }
}
