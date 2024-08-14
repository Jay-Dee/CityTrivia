using CityTrivia.DataAccess.Contexts;

namespace CityTrivia.DataAccess.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;
        private CitiesTriviaDbContext context;
        public ICitiesRepository CitiesRepository { get; private set; }
        

        public UnitOfWork(ICitiesRepository citiesRepository, CitiesTriviaDbContext dbContext) {
            CitiesRepository = citiesRepository ?? new CitiesRepository(dbContext);
            context = dbContext;
        }

        public async Task<bool> CompleteWork()
        {
            return await context.SaveChangesAsync() > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;

            }

            // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
            // ~UnitOfWork()
            // {
            //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            //     Dispose(disposing: false);
            // }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
