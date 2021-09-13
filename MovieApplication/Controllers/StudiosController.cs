using Microsoft.AspNetCore.Mvc;
using MovieApplication.Domain.Repositories;
using MovieApplication.Infrastructure.Dto;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieApplication.Controllers
{
    [Route("[controller]")]
    public class StudiosController : Controller
    {
        private readonly IStudioRepository _repository;

        public StudiosController(IStudioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var producers = await _repository.List(cancellationToken);
            return Ok(
                producers
                    .Select(e => new StudioDto(e))
                    .OrderBy(e => e.Name)
            );
        }
    }
}
