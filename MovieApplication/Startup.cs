using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieApplication.Application.Repositories;
using MovieApplication.Domain;
using MovieApplication.Domain.Repositories;
using MovieApplication.Infrastructure;
using MovieApplication.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MovieApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    // Formatacao identada apenas para facilitar visualizacao
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };
                });

            services.AddDbContext<IAwardDbContext, AwardContext>();
            services.AddScoped<IAwardNomineeRepository, DbAwardNomineeRepository>();
            services.AddScoped<IProducerRepository, DbProducerRepository>();
            services.AddScoped<IStudioRepository, DbStudioRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.InitializeData(); // inicializa o banco de dados

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
