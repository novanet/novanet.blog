using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherForecast.Domain.ValueObjects;

namespace WeatherForecast.Database.Converters;

public class SummaryConverter : ValueConverter<Summary, string>
{
    public SummaryConverter() : base(
        v => v.Value,
        v => new Summary(v))
    { }
}