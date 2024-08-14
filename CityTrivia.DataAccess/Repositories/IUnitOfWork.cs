namespace CityTrivia.DataAccess.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICitiesRepository CitiesRepository { get; }
        Task<bool> CompleteWork();
    }
}
