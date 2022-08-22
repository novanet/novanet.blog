using Microsoft.EntityFrameworkCore;
using WeatherForecast.Database.Configuration;

namespace WeatherForecast.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.WeatherForecast> WeatherForecasts { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new WeatherForecastConfiguration());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // Register global converters
                
    }
}

//var summaries = new[]
//       {
//            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//        };

//        return Enumerable.Range(1, 5).Select(index =>
//           new WeatherForecast
//           (
//               DateTime.Now.AddDays(index),
//               Random.Shared.Next(-20, 55),
//               summaries[Random.Shared.Next(summaries.Length)]
//           ))
//            .ToArray();