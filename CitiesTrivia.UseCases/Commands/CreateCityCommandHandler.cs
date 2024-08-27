using MediatR;
using CityTrivia.DataAccess.Repositories;

namespace CitiesTrivia.UseCases.Commands
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, bool>
    {
        private readonly IUnitOfWork _worker;

        public CreateCityCommandHandler(IUnitOfWork worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }


        public Task<bool> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            _worker.CitiesRepository.Add(request.CityToAdd);
            return _worker.CompleteWork();
        }
    }
}
