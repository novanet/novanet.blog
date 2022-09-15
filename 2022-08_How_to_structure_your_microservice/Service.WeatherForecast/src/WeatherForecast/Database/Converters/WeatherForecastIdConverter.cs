using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherForecast.Domain.ValueObjects;

namespace WeatherForecast.Database.Converters;

public class WeatherForecastIdConverter : ValueConverter<WeatherForecastId, Guid>
{
    public WeatherForecastIdConverter() : base(
        v => v.Value,
        v => new WeatherForecastId(v))
    { }
}