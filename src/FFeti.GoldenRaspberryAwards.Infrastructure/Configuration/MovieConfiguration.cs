using FFeti.GoldenRaspberryAwards.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FFeti.GoldenRaspberryAwards.Infrastructure.Data.Configuration;
internal sealed class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        //builder.ToTable("Movies");
        builder.HasKey(m => m.Id);        
        builder.HasIndex(m => m.Winner);
    }
}
