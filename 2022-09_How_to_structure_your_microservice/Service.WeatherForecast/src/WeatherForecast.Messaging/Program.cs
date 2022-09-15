using Microsoft.Extensions.Hosting;

await new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .Build()
    .RunAsync();
