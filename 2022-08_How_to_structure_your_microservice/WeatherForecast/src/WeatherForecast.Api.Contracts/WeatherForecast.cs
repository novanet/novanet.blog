namespace WeatherForecast.Api.Contracts;

public record WeatherForecast(
    Guid Id,
    DateTime Date,
    int Temperature,
    string? Summary);