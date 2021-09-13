using MovieApplication.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MovieApplication.Domain.Repositories
{
    public interface IAwardNomineeRepository
    {
        public Task<AwardNominee> GetWinnerOfTheYear(int year, CancellationToken cancellationToken = default);
        public Task<List<AwardNominee>> List(CancellationToken cancellationToken = default);
        public Task<List<AwardNominee>> ListWinners(CancellationToken cancellationToken = default);
    }
}
