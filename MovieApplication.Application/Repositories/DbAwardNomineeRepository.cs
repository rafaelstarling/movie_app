using Microsoft.EntityFrameworkCore;
using MovieApplication.Domain;
using MovieApplication.Domain.Models;
using MovieApplication.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieApplication.Application.Repositories
{
    public class DbAwardNomineeRepository : IAwardNomineeRepository
    {
        private readonly IAwardDbContext _awardContext;

        public DbAwardNomineeRepository(IAwardDbContext awardContext)
        {
            _awardContext = awardContext;
        }

        public async Task<AwardNominee> GetWinnerOfTheYear(int year, CancellationToken cancellationToken = default)
        {
            var winners = await ListWinners(cancellationToken);
            return winners.FirstOrDefault(e => e.Year == year);
        }

        public Task<List<AwardNominee>> List(CancellationToken cancellationToken = default)
        {
            return _awardContext.AwardNominees
                .Include(e => e.Producers)
                .Include(e => e.Studios)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<AwardNominee>> ListWinners(CancellationToken cancellationToken = default)
        {
            var all = await List(cancellationToken);
            return all.Where(e => e.IsWinner).ToList();
        }
    }
}
