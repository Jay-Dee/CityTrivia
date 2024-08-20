using MediatR;
using CityTrivia.DataAccess.Repositories;

namespace CitiesTrivia.UseCases.Commands
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, bool>
    {
        private readonly IUnitOfWork _worker;

        public CreateCountryCommandHandler(IUnitOfWork worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }


        public Task<bool> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            _worker.CountriesRepository.Add(request.CountryToAdd);
            return _worker.CompleteWork();
        }
    }
}
