using Microsoft.AspNetCore.Mvc;
using MovieApplication.Domain.Repositories;
using MovieApplication.Infrastructure.Dto;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieApplication.Controllers
{
    [Route("[controller]")]
    public class ProducersController : Controller
    {
        private readonly IProducerRepository _repository;

        public ProducersController(IProducerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var producers = await _repository.List(cancellationToken);
            return Ok(
                producers
                    .Select(e => new ProducerDto(e))
                    .OrderBy(e => e.Name)
            );
        }

        [HttpGet]
        [Route("minmaxvictories")]
        public async Task<IActionResult> MinMaxConsecutiveVictories(CancellationToken cancellationToken)
        {
            var minMaxVictories = await _repository.GetMinMaxProducerConsecutiveVictories(cancellationToken);
            return Ok(
                new MinMaxProducerConsecutiveVictoriesDto(minMaxVictories)
            );
        }
    }
}
