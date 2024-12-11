using FFeti.GoldenRaspberryAwards.Domain.Interfaces;
using FFeti.GoldenRaspberryAwards.Infrastructure.Data.Database;
using FFeti.GoldenRaspberryAwards.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FFeti.GoldenRaspberryAwards.Infrastructure.Data;
public static class DependencyInjection
{
    public static void AddInfratructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MoviesConnection");
        
        var path = Path.Combine("Data", "movielist.csv");
        services.AddScoped<IGoldenRaspberryCsvContext, GoldenRaspberryCsvContext>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddDbContext<GoldenRaspberryDbContext>(options => options.UseSqlite(connectionString));


    }
}
