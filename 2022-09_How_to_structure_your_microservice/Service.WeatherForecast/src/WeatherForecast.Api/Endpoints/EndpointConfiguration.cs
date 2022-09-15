using WeatherForecast.Api.Endpoints.GetWeatherForecast;

namespace WeatherForecast.Api;

public static class EndpointConfiguration
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        app.MapGet("/weatherforecast", GetWeatherForecastEndpoint.Execute);
        app.MapPut("/weatherforecast/{id:guid}", GetWeatherForecastEndpoint.Execute);
    }
}
