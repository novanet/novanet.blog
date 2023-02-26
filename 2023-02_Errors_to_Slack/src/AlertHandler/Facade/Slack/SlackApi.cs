using System.Text.Json;
using System.Text;

namespace AlertHandler.Facade.Slack
{
    public interface ISlackApi
    {
        Task Send(string text);
    }

    public class SlackApi : ISlackApi
    {
        private readonly HttpClient _httpClient;
        private readonly SlackConfiguration _configuration;

        public SlackApi(HttpClient httpClient, SlackConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task Send(string text)
        {
            await _httpClient.PostAsync(
                requestUri: _configuration.ExceptionsWebHook,
                content: new StringContent(JsonSerializer.Serialize(new Payload(text)), Encoding.UTF8, "application/json"));
        }
    }
}
