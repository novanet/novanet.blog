using System.Net;
using System.Text.Json;
using AlertHandler.Facade.ApplicationInsights;
using AlertHandler.Facade.Slack;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AlertHandler.Features.HandleExceptions
{
    public class HttpTrigger
    {
        private readonly ISlackApi _slackApi;
        private readonly ILogger _logger;

        public HttpTrigger(ILogger<HttpTrigger> logger, ISlackApi slackApi)
        {
            _logger = logger;
            _slackApi = slackApi;
        }

        [Function("HandleExceptions")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "exceptions")] HttpRequestData req)
        {
            var alert = JsonSerializer
                .Deserialize<LogAlert>(await new StreamReader(req.Body)
                .ReadToEndAsync())!;

            _logger.LogInformation($"Exception alert: {JsonSerializer.Serialize(alert)}");

            await _slackApi.Send(BuildText(alert));

            return req.CreateResponse(HttpStatusCode.NoContent);
        }

        private static string BuildText(LogAlert alert)
        {
            // Adding an emoji because I can, and to give some life to the alert
            return $":boom: *app*: New errors ({alert!.GetErrorCount()}):\t" + $"<{alert!.LinkToSearchResults()}|{alert!.ExceptionMessage()}>";
        }
    }
}
