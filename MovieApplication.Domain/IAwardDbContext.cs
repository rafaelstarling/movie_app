using Microsoft.EntityFrameworkCore;
using MovieApplication.Domain.Models;

namespace MovieApplication.Domain
{
    public abstract class IAwardDbContext : DbContext
    {
        public abstract void Seed();
        public virtual DbSet<AwardNominee> AwardNominees { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Studio> Studios { get; set; }
    }
}
