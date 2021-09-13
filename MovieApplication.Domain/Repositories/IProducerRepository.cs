using MovieApplication.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MovieApplication.Domain.Repositories
{
    public interface IProducerRepository
    {
        public Task<MinMaxProducerConsecutiveVictories> GetMinMaxProducerConsecutiveVictories(
            CancellationToken cancellationToken = default
        );

        public Task<List<Producer>> List(CancellationToken cancellationToken = default);
    }
}
