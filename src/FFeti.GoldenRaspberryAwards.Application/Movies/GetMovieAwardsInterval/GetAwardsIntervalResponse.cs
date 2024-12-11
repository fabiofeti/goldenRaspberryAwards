namespace FFeti.GoldenRaspberryAwards.Application.Movies.GetAwardsInterval;

public record GetAwardsIntervalResponse
{
    public List<AwardIntervalResponse> Min { get; set; }
    public List<AwardIntervalResponse> Max { get; set; }
    public GetAwardsIntervalResponse(List<AwardIntervalResponse> min, List<AwardIntervalResponse> max)
    {
        Min = min;
        Max = max;
    }

    public GetAwardsIntervalResponse()
    {
    }
}

