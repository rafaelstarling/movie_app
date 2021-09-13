using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MovieApplication.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new
            {
                AvailableRoutes = new[]
                {
                    new {
                        Url = "/",
                        HttpMethod = "GET",
                        Description = "Lista todas as rotas da api para facilitar teste",
                    },
                    new {
                        Url = "/producers/",
                        HttpMethod = "GET",
                        Description = "Lista os produtores com os anos em que eles receberam a premiação",
                    },
                    new {
                        Url = "/producers/minmaxvictories/",
                        HttpMethod = "GET",
                        Description = "Lista os produtores com os menores e maiores intervalos entre prêmios consecutivos",
                    },
                    new {
                        Url = "/studios/",
                        HttpMethod = "GET",
                        Description = "Lista os estúdios com os anos em que eles receberam a premiação",
                    },
                    new {
                        Url = "/awards/",
                        HttpMethod = "GET",
                        Description = "Lista os filmes que foram premiados",
                    },
                    new {
                        Url = "/awards/nominees/",
                        HttpMethod = "GET",
                        Description = "Lista todos os filmes",
                    },
                    new {
                        Url = "/awards/{year}/",
                        HttpMethod = "GET",
                        Description = "Lista o filme premiado no ano {year}",
                    },
                }.OrderBy(e => e.Url)
            });
        }
    }
}
