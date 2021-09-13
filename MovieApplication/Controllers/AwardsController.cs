using Microsoft.AspNetCore.Mvc;
using MovieApplication.Domain.Repositories;
using MovieApplication.Infrastructure.Dto;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AwardsController : Controller
    {
        private readonly IAwardNomineeRepository _repository;

        public AwardsController(IAwardNomineeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var producers = await _repository.ListWinners(cancellationToken);
            return Ok(
                producers
                    .Select(e => new AwardNomineeDto(e))
                    .OrderByDescending(e => e.Year)
                    .ThenBy(e => e.Title)
            );
        }

        [HttpGet]
        [Route("nominees")]
        public async Task<IActionResult> ListAll(CancellationToken cancellationToken)
        {
            var producers = await _repository.List(cancellationToken);
            return Ok(
                producers
                    .Select(e => new AwardNomineeDto(e))
                    .OrderByDescending(e => e.Year)
                    .ThenBy(e => e.Title)
            );
        }

        [HttpGet]
        [Route("{year}")]
        public async Task<IActionResult> WinnerOfTheYear(int year, CancellationToken cancellationToken)
        {
            var winner = await _repository.GetWinnerOfTheYear(year, cancellationToken);

            if (winner == null)
            {
                return NotFound(new 
                { 
                    Error = $"Não foi possível encontrar o vencedor do ano {year}" 
                });
            }

            return Ok(new AwardNomineeDto(winner));
        }
    }
}
