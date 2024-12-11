using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using FFeti.GoldenRaspberryAwards.Domain;
using FFeti.GoldenRaspberryAwards.Domain.Interfaces;
using FFeti.GoldenRaspberryAwards.Infrastructure.Data.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace FFeti.GoldenRaspberryAwards.Infrastructure.Data.Database;
public sealed class GoldenRaspberryCsvContext: IGoldenRaspberryCsvContext
{
    private readonly string _moviesCsvPath;
    private readonly Lazy<List<Movie>> _movies;

    public GoldenRaspberryCsvContext(IWebHostEnvironment webHostEnvironment)
    {
        _moviesCsvPath = Path.Combine(webHostEnvironment.ContentRootPath, "Data", "movielist.csv");        
        _movies = new Lazy<List<Movie>>(() => LoadCsv<Movie, MovieCsvMap>(_moviesCsvPath));
    }
    private List<T> LoadCsv<T, TClassMap>(string path) where TClassMap : ClassMap<T>
    {

        using var reader = new StreamReader(path);
        using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", Encoding = Encoding.UTF8 });
        csvReader.Context.RegisterClassMap<TClassMap>();
        return csvReader.GetRecords<T>().ToList();
    }

    public List<Movie> Movies => _movies.Value;
}
