namespace WeatherForecast.Api.Endpoints.GetWeatherForecast;

public static class Mapping
{
    public static IList<Contracts.WeatherForecast> ToContract(this IList<Domain.WeatherForecast> domain) =>
        domain.Select(x => x.ToContract()).ToList();

    private static Contracts.WeatherForecast ToContract(this Domain.WeatherForecast domain) =>
        new Contracts.WeatherForecast(
            domain.Id.Value,
            domain.Date,
            domain.Temperature.Value,
            domain.Summary is not null ? domain.Summary.Value : null);
}
