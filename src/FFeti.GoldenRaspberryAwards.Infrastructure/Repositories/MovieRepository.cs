using FFeti.GoldenRaspberryAwards.Domain;
using FFeti.GoldenRaspberryAwards.Domain.Interfaces;
using FFeti.GoldenRaspberryAwards.Infrastructure.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace FFeti.GoldenRaspberryAwards.Infrastructure.Data.Repositories;
internal sealed class MovieRepository(GoldenRaspberryDbContext context) : IMovieRepository
{
    public async Task AddRange(IEnumerable<Movie> movies)
    {
        await context.Movies.AddRangeAsync(movies);        
    }

    public bool HasAny()
    {
        return context.Movies.Any();
    }

    public async Task<IEnumerable<Movie>> GetWinnersMovies()
    {
        return await context.Movies
            .Where(m => m.Winner)
            .ToListAsync();
    }

    public IEnumerable<Movie> GetAll()
    {
        return context.Movies.ToList();
    }

    public async Task SaveChangesAsync()
    {
       await context.SaveChangesAsync();
    }
}
