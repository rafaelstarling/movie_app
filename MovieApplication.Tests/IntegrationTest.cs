using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MovieApplication.Domain;
using System.Net.Http;

namespace MovieApplication.Tests
{
    internal abstract class IntegrationTest<TDbMock> where TDbMock : IAwardDbContext
    {
        protected readonly HttpClient _client;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(IAwardDbContext));
                        services.AddDbContext<IAwardDbContext, TDbMock>();
                    });
                });
            _client = appFactory.CreateClient();
        }
    }
}
