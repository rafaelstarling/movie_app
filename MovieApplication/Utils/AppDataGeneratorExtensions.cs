using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MovieApplication.Domain;

namespace MovieApplication.Utils
{
    public static class AppDataGeneratorExtensions
    {
        public static IApplicationBuilder InitializeData(this IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                using var context = serviceScope.ServiceProvider.GetService<IAwardDbContext>();
                context.Seed();
            }

            return builder;
        }
    }
}
