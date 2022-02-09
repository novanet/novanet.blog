using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace QueueReader
{
    public class ProcessQueueItem
    {
        [Function("ProcessQueueItem")]
        public void Run([ServiceBusTrigger("%QueueName%", Connection = "ServiceBusConnection")] string myQueueItem, ILogger _logger)
        {
            _logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
