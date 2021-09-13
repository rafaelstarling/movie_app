using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieApplication.Domain;
using MovieApplication.Domain.Models;
using MovieApplication.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieApplication.Application.Repositories
{
    public class DbProducerRepository : IProducerRepository
    {
        private readonly IAwardDbContext _awardContext;
        private readonly ILogger<DbProducerRepository> _logger;

        public DbProducerRepository(IAwardDbContext awardContext, ILogger<DbProducerRepository> logger)
        {
            _awardContext = awardContext;
            _logger = logger;
        }

        public async Task<MinMaxProducerConsecutiveVictories> GetMinMaxProducerConsecutiveVictories(
            CancellationToken cancellationToken = default
        )
        {
            var multiChampionProducers = (await List(cancellationToken))
                .Where(p => p.Nominations.Where(n => n.IsWinner).Count() >= 2);

            var minMaxBuilder = new MinMaxProducerConsecutiveVictories.Builder();

            foreach (var producer in multiChampionProducers)
            {
                var producerWinnings = producer.Nominations
                    .Where(n => n.IsWinner)
                    .OrderBy(n => n.Year)
                    .ToList();

                for (int i = 1; i < producerWinnings.Count; i++)
                {
                    _logger.LogInformation("Verificando produtor: {Producer}. Vitórias: {FirstWin} e {SecondWin}", producer, producerWinnings[i - 1], producerWinnings[i]);
                    minMaxBuilder.CheckProducerAwards(producer, producerWinnings[i - 1], producerWinnings[i]);
                }
            }

            return minMaxBuilder.Build();
        }

        public Task<List<Producer>> List(CancellationToken cancellationToken = default)
        {
            return _awardContext.Producers
                .Include(e => e.Nominations)
                .ToListAsync(cancellationToken);
        }
    }
}
