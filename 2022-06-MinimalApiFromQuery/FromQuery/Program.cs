using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();
app.UseHttpsRedirection();
app.MapGet("/", (QueryParameters? parameters) => parameters);

app.Run();


public record QueryParameters(IReadOnlyList<string>? Ids, int? Skip, int? Take)
{
    public static ValueTask<QueryParameters?> BindAsync(HttpContext context)
        => ValueTask.FromResult<QueryParameters?>(new (
            Ids: !string.IsNullOrEmpty(context.Request.Query["ids"]) ? context.Request.Query["ids"].ToString().Split(',').ToList().AsReadOnly() : null,
            Skip: int.TryParse(context.Request.Query["skip"], out var skip) ? skip : null,
            Take: int.TryParse(context.Request.Query["take"], out var take) ? take : null));
}