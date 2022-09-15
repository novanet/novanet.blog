using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherForecast.Domain.ValueObjects;

namespace WeatherForecast.Database.Converters;

public class TemperatureConverter : ValueConverter<Temperature, int>
{
    public TemperatureConverter() : base(
        v => v.Value,
        v => new Temperature(v))
    { }
}