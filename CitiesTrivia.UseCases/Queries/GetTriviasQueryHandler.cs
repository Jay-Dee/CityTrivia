using MediatR;
using CityTrivia.DataAccess.Entities;
using CityTrivia.DataAccess.Repositories;

namespace CitiesTrivia.UseCases.Queries
{
    public class GetTriviasQueryHandler : IRequestHandler<GetTriviasQuery, IEnumerable<Trivia>>
    {
        private readonly IUnitOfWork _worker;

        public GetTriviasQueryHandler(IUnitOfWork worker)
        {
            _worker = worker;
        }

        public async Task<IEnumerable<Trivia>> Handle(GetTriviasQuery request, CancellationToken cancellationToken)
        {
            return await _worker.TriviasRepository.GetAllAsync(request.NameToFilter, request.ToSkip, request.ToTake);
        }
    }
}
