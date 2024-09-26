namespace CityTrivia.DataAccess.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(string? nameToFilter, int skipCount, int takeCount);

        Task<TEntity?> GetAsync(int entityId);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        Task<bool> SaveChangesAsync();
    }
}
