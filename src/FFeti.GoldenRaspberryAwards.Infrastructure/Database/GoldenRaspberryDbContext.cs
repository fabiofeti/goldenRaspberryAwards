using FFeti.GoldenRaspberryAwards.Domain;
using FFeti.GoldenRaspberryAwards.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FFeti.GoldenRaspberryAwards.Infrastructure.Data.Database;
public class GoldenRaspberryDbContext(DbContextOptions<GoldenRaspberryDbContext> options) : DbContext(options)
{
    public DbSet<Movie> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
    }


}

