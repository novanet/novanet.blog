using Microsoft.AspNetCore.Mvc;
using webapi_in_docker_service;

namespace webapi_in_docker_app_service.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private WeatherService _service;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        _service = new WeatherService();
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return _service.GetWeather();
    }
}
