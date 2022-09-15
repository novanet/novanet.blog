using WeatherForecast.Api.Contracts;
using WeatherForecast.Features.UpdateWeatherForecast;

namespace WeatherForecast.Api.Endpoints.GetWeatherForecast;

public static class UpdateWeatherForecastEndpoint
{
    public static async Task<IResult> Execute(
        UpdateWeatherForecastFeature feature,
        UpdateWeatherForecastCommand command,
        Guid id)
    {
        // Add validation here, i.e. using FluentValidation

        await feature.UpdateWeatherForecast(
            new Domain.ValueObjects.WeatherForecastId(id), 
            new Domain.ValueObjects.Temperature(command.Temperature));

        return Results.NoContent();
    }
}
