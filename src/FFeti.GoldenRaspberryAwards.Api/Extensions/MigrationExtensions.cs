using FFeti.GoldenRaspberryAwards.Infrastructure.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace FFeti.GoldenRaspberryAwards.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {

        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using GoldenRaspberryDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<GoldenRaspberryDbContext>();
        dbContext.Database.EnsureCreated();
       
    }
}
