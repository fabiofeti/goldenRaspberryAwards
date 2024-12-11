using CsvHelper.Configuration;
using FFeti.GoldenRaspberryAwards.Domain;

namespace FFeti.GoldenRaspberryAwards.Infrastructure.Data.Configuration;
public class MovieCsvMap : ClassMap<Movie>
{
    public MovieCsvMap()
    {
        Map(m => m.Year).Name("year");
        Map(m => m.Title).Name("title");
        Map(m => m.Studios).Name("studios");
        Map(m => m.Producers).Name("producers");
        Map(m => m.Winner)
           .Convert(item =>
           {
              
               if (item.Row.TryGetField("winner", out string value))
               {
                   return string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase);
               }
               return false;
           });

    }
}
