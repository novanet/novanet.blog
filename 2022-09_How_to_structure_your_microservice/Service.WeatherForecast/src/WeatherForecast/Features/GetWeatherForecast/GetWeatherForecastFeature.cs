using Microsoft.EntityFrameworkCore;
using WeatherForecast.Database;

namespace WeatherForecast.Features.GetWeatherForecast;

public class GetWeatherForecastFeature
{
    private readonly AppDbContext _db;

    public GetWeatherForecastFeature(AppDbContext db) => _db = db;

    public async Task<(FeatureResult, IList<Domain.WeatherForecast>)> GetWeatherForecast()
    {
        var forecasts = await _db.WeatherForecasts.ToListAsync();

        return (FeatureResult.Success, forecasts);
    }
}
