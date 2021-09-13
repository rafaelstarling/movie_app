using Microsoft.EntityFrameworkCore;
using MovieApplication.Domain;
using MovieApplication.Domain.Models;
using MovieApplication.Domain.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MovieApplication.Application.Repositories
{
    public class DbStudioRepository : IStudioRepository
    {
        private readonly IAwardDbContext _awardContext;

        public DbStudioRepository(IAwardDbContext awardContext)
        {
            _awardContext = awardContext;
        }

        public Task<List<Studio>> List(CancellationToken cancellationToken = default)
        {
            return _awardContext.Studios
                .Include(e => e.Nominations)
                .ToListAsync(cancellationToken);
        }
    }
}
