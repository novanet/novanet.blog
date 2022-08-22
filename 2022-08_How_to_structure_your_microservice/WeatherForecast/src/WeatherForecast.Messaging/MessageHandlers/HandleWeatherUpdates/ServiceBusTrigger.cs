using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace WeatherForecast.Messaging
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("HandleWeatherUpdates")]
        public void Run([ServiceBusTrigger("myqueue", Connection = "")] string myQueueItem)
        {
            _logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
