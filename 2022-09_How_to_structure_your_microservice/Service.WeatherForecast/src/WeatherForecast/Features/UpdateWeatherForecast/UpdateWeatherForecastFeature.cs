using Microsoft.EntityFrameworkCore;
using WeatherForecast.Database;
using WeatherForecast.Domain.ValueObjects;

namespace WeatherForecast.Features.UpdateWeatherForecast;
public class UpdateWeatherForecastFeature
{
    private readonly AppDbContext _db;

    public UpdateWeatherForecastFeature(AppDbContext db) => _db = db;

    public async Task UpdateWeatherForecast(WeatherForecastId id, Temperature newTemperature)
    {
        var forecast = await _db.WeatherForecasts.SingleAsync(x => x.Id == id);

        // The domain exposes methods for operations to the domain
        // It's not allowed to set the forecast.Temperature directly in the feature
        forecast.ChangeTemperature(newTemperature);

        await _db.SaveChangesAsync();        
    }
}
