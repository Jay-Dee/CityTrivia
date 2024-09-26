namespace CityTrivia.DataAccess.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICitiesRepository CitiesRepository { get; }
        ICountriesRepository CountriesRepository { get; }
        ITriviasRepository TriviasRepository { get; }
        Task<bool> CompleteWork();
    }
}
