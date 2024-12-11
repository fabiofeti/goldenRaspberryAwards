using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFeti.GoldenRaspberryAwards.Application.Movies.GetAwardsInterval;
using Microsoft.Extensions.DependencyInjection;

namespace FFeti.GoldenRaspberryAwards.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddScoped<IGetAwardsUseCase, GetAwardsUseCase>();

        return services;
    }
}
