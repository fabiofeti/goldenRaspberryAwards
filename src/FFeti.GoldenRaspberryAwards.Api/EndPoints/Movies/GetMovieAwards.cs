
using FFeti.GoldenRaspberryAwards.Application.Movies.GetAwardsInterval;
using FluentResults;
using MediatR;

namespace FFeti.GoldenRaspberryAwards.Api.EndPoints.Movies;

public sealed class GetMovieAwards : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/movies/awards-winners", async (ISender sender) =>
        {            
            var query = new GetAwardsIntervalQuery();
            Result<GetAwardsIntervalResponse> response= await sender.Send(query);
            return Results.Ok(response.Value);
        });
    }
}
