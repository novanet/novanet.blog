namespace WeatherForecast.Domain.ValueObjects;

public record WeatherForecastId(Guid Value)
{
    public static WeatherForecastId NewId() => new(Guid.NewGuid());
};