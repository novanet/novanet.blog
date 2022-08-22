using WeatherForecast.Domain.ValueObjects;

namespace WeatherForecast.Domain;

public class WeatherForecast
{
    public WeatherForecastId Id { get; private init; } = null!;

    public DateTime Date { get; private init; }

    public Temperature Temperature { get; private set; } = null!;

    public Summary? Summary { get; private set; }

    public WeatherForecast(DateTime date, Temperature temperature, Summary? summary)
    {
        Id = WeatherForecastId.NewId();
        Date = date;
        Temperature = temperature;
        Summary = summary;
    }
}
