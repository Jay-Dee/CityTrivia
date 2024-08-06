using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTrivia.DataAccess.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICitiesRepository CitiesRepository { get; }
        Task<bool> CompleteWork();
    }
}
