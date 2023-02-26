using System.Text.Json.Serialization;

namespace AlertHandler.Facade.Slack
{
    public record Payload([property: JsonPropertyName("text")] string Text);
}
