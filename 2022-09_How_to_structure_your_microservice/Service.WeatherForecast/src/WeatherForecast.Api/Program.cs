using Microsoft.EntityFrameworkCore;
using WeatherForecast.Api;
using WeatherForecast.Database;
using WeatherForecast.Features.GetWeatherForecast;
using WeatherForecast.Features.UpdateWeatherForecast;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database
builder.Services.AddDbContext<AppDbContext>((DbContextOptionsBuilder options) =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly(typeof(AppDbContext).Namespace)));

// Register features
builder.Services.AddScoped<GetWeatherForecastFeature>();
builder.Services.AddScoped<UpdateWeatherForecastFeature>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.RegisterEndpoints();
app.MapHealthChecks("/healthz");
app.MapGet("/", () => string.Empty);


app.Run();

