using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherForecast.Database.Converters;

namespace WeatherForecast.Database.Configuration;

public class WeatherForecastConfiguration : IEntityTypeConfiguration<Domain.WeatherForecast>
{
    public void Configure(EntityTypeBuilder<Domain.WeatherForecast> builder)
    {
        builder
            .Property(x => x.Id)
            .HasConversion(typeof(WeatherForecastIdConverter));

        builder
            .Property(x => x.Temperature)
            .IsRequired()
            .HasConversion(typeof(TemperatureConverter));

        builder
            .Property(x => x.Summary)
            .HasMaxLength(512)
            .HasConversion(typeof(SummaryConverter));
    }
}
