using FFeti.GoldenRaspberryAwards.Domain.Interfaces;
using FluentResults;
using MediatR;

namespace FFeti.GoldenRaspberryAwards.Application.Movies.GetAwardsInterval;
public class GetAwardsIntervalQueryHandler(IGetAwardsUseCase getAwardsUseCase) : IRequestHandler<GetAwardsIntervalQuery, Result<GetAwardsIntervalResponse>>
{

    public async Task<Result<GetAwardsIntervalResponse>> Handle(GetAwardsIntervalQuery request, CancellationToken cancellationToken)
    {
        var response = await getAwardsUseCase.ExcuteAsync();
        return Result.Ok(response);
    }
}
