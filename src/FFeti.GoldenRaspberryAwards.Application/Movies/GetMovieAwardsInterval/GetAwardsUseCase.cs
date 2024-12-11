using FFeti.GoldenRaspberryAwards.Domain;
using FFeti.GoldenRaspberryAwards.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FFeti.GoldenRaspberryAwards.Application.Movies.GetAwardsInterval;
public class GetAwardsUseCase(
    IMovieRepository movieRepository,
    IGoldenRaspberryCsvContext goldenRaspberryCsvContext,    
    ILogger<GetAwardsUseCase> logger
    ) : IGetAwardsUseCase
{
    
    private readonly IMovieRepository _movieRepository = movieRepository;

    
    public async Task<GetAwardsIntervalResponse> ExcuteAsync()
    {
        try
        {
            await LoadMoviesFromCsv();            
            var winners = await _movieRepository.GetWinnersMovies();

            var producerWins = winners
            .Where(m => m.Winner)
            .SelectMany(m => m.Producers
                               .Split(new[] {", ", " and "}, StringSplitOptions.RemoveEmptyEntries)
                               .Select(p => new { Producer = p.Trim(), m.Year }))
            .GroupBy(x => x.Producer, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Year).Distinct().OrderBy(y => y).ToList(), StringComparer.OrdinalIgnoreCase);
            var intervals = new List<AwardIntervalResponse>();

            foreach (var kvp in producerWins)
            {
                var producer = kvp.Key;
                var years = kvp.Value;

                if (years.Count < 2)
                {
                    continue;
                }                    

                for (int i = 0; i < years.Count - 1; i++)
                {
                    var interval = years[i + 1] - years[i];
                    intervals.Add(new AwardIntervalResponse(
                        Producer: producer,
                        Interval: interval,
                        PreviousWin: years[i],
                        FollowingWin: years[i + 1]
                    ));
                }
            }

            if (!intervals.Any())
            {
                return new GetAwardsIntervalResponse(new List<AwardIntervalResponse>(), new List<AwardIntervalResponse>());
            }

            var minInterval = intervals.Min(i => i.Interval);
            var maxInterval = intervals.Max(i => i.Interval);

            var minList = intervals.Where(i => i.Interval == minInterval).ToList();
            var maxList = intervals.Where(i => i.Interval == maxInterval).ToList();

            return new GetAwardsIntervalResponse(minList, maxList);
            
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while trying to get the awards interval {ErrorMessage}", ex.Message);
            throw;
        }
    }

    private async Task LoadMoviesFromCsv()
    {
        if (_movieRepository.HasAny())
        {
            return;
        }
        await _movieRepository.AddRange(goldenRaspberryCsvContext.Movies);
        await _movieRepository.SaveChangesAsync();
    }
}

public interface IGetAwardsUseCase
{
    Task<GetAwardsIntervalResponse> ExcuteAsync();
}
