var builder = WebApplication.CreateBuilder(args);

// Requires nuget Microsoft.ApplicationInsights.AspNetCore
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("/", () => 
{
    throw new Exception(
        "This text should appear in Slack!");
});

app.Run();