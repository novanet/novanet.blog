using AlertHandler.Facade.Slack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((builder, services) =>
    {
        services.AddSingleton(new SlackConfiguration
        {
            ExceptionsWebHook = builder.Configuration["SlackExceptionsWebHook"]
        });

        services.AddHttpClient<ISlackApi, SlackApi>(client =>
        {
            client.BaseAddress = new Uri("https://hooks.slack.com");
        });
    })
    .Build()
    .RunAsync();
