namespace FFeti.GoldenRaspberryAwards.Application.Movies.GetAwardsInterval;

public record AwardIntervalResponse(string Producer, int Interval, int PreviousWin, int FollowingWin);
