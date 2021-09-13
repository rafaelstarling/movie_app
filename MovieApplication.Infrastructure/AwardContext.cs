using Microsoft.EntityFrameworkCore;
using MovieApplication.Domain;
using MovieApplication.Infrastructure.Data;

namespace MovieApplication.Infrastructure
{
    public class AwardContext : IAwardDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("awards");
        }

        public override void Seed()
        {
            new DataGenerator(this).ImportFromFile(@"Resources/movielist.csv");
            this.SaveChanges();
        }
    }
}
