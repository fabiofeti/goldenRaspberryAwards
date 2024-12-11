using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.Builder;
using FFeti.GoldenRaspberryAwards.Api.Extensions;
using FFeti.GoldenRaspberryAwards.Api.Infrastructure;
using FFeti.GoldenRaspberryAwards.Api.OpenApi;
using FFeti.GoldenRaspberryAwards.Application;
using FFeti.GoldenRaspberryAwards.Infrastructure.Data;
using FFeti.GoldenRaspberryAwards.Infrastructure.Data.Database;
using Serilog;
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(context.Configuration));
       
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //var connectionString = builder.Configuration.GetConnectionString("MoviesConnection");
        //builder.Services.AddDbContext<GoldenRaspberryDbContext>(options => options.UseSqlite(connectionString));

        builder.Services
            .AddApplication()
            .AddInfratructure(builder.Configuration);

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>();
        builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());


        var app = builder.Build();

        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
        .HasApiVersion(new ApiVersion(1))
       .ReportApiVersions()
        .Build();

        RouteGroupBuilder versionedGroup = app
            .MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(apiVersionSet);

        app.MapEndpoints(versionedGroup);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var descriptions = app.DescribeApiVersions();

                foreach (var description in descriptions)
                {
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant();

                    options.SwaggerEndpoint(url, name);
                }
            });

            //app.ApplyMigrations();
        }

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<GoldenRaspberryDbContext>();
            dbContext.Database.EnsureCreated();
        }

        app.UseHttpsRedirection();


        app.Run();
    }
}
