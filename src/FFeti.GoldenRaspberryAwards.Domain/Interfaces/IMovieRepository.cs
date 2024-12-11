namespace FFeti.GoldenRaspberryAwards.Domain.Interfaces;
public interface IMovieRepository
{
    Task AddRange(IEnumerable<Movie> movies);
    bool HasAny();
    Task<IEnumerable<Movie>> GetWinnersMovies();
    IEnumerable<Movie> GetAll();
    Task SaveChangesAsync();


}
