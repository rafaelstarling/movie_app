using MovieApplication.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MovieApplication.Domain.Repositories
{
    public interface IStudioRepository
    {
        public Task<List<Studio>> List(CancellationToken cancellationToken = default);
    }
}
