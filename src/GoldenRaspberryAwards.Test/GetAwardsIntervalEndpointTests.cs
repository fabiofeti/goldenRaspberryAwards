using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;


namespace GoldenRaspberryAwards.Test;

public class MoviesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task GetIntervals_ReturnsOk_WhenDataExists()
    {
        await using var application = new ApplicationServices();

        using var client = application.CreateClient();

        client.BaseAddress = new Uri("http://localhost:7087");

        //Act :
        var response = await client.GetAsync("/api/v1/movies/awards-winners");

        // Assert:
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetIntervals_ReturnsNotFound_WhenDataDoesNotExist()
    {
        await using var application = new ApplicationServices();

        using var client = application.CreateClient();

        client.BaseAddress = new Uri("http://localhost:7087");

        //Act :
        var response = await client.GetAsync("/api/v1/movies/non-existent");

        // Assert:
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}

internal class ApplicationServices : WebApplicationFactory<Program>
{
    private readonly string _environment;

    public ApplicationServices(string environment = "Development")
    {
        _environment = environment;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(_environment);

        // Add mock/test services to the builder here
        builder.ConfigureServices(services => services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();
                return connection;
            }));

        return base.CreateHost(builder);
    }
}

