using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace QueueReader
{
public class ProcessQueueItem
{
    private readonly ILogger _logger;
    public ProcessQueueItem(ILoggerFactory loggerFactory) => _logger = loggerFactory.CreateLogger<ProcessQueueItem>();

    [Function("ProcessQueueItem")]
    public void Run([ServiceBusTrigger("%QueueName%", Connection = "ServiceBusConnection")] string myQueueItem)
    {
        _logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
    }
}
}
