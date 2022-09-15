using WeatherForecast.Features.GetWeatherForecast;

namespace WeatherForecast.Api.Endpoints.GetWeatherForecast;

public static class GetWeatherForecastEndpoint
{
    public static async Task<IResult> Execute(GetWeatherForecastFeature feature)
    {
        var (result, forecasts) = await feature.GetWeatherForecast();

        return result switch
        {
            FeatureResult.Success => Results.Ok(forecasts.ToContract()),
            FeatureResult.InvalidRequest => Results.Problem(nameof(FeatureResult.InvalidRequest), statusCode: StatusCodes.Status400BadRequest),

            _ => throw new NotImplementedException()
        };          
    }
}
