using MediatR;
using CityTrivia.DataAccess.Entities;
using CityTrivia.DataAccess.Repositories;

namespace CitiesTrivia.UseCases.Queries
{
    public class GetTriviaQueryHandler : IRequestHandler<GetTriviaQuery, Trivia>
    {
        private readonly IUnitOfWork _worker;

        public GetTriviaQueryHandler(IUnitOfWork worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }

        public async Task<Trivia> Handle(GetTriviaQuery request, CancellationToken cancellationToken)
        {
            return await _worker.TriviasRepository.GetAsync(request.TriviaId);
        }
    }
}
