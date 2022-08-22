namespace WeatherForecast.Domain.ValueObjects;

public record Temperature(int Value)
{
    public int TemperatureF => 32 + (int)(Value / 0.5556);
}