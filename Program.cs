using KafkaProducerConsumer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<KafkaProducer>();
        services.AddHostedService<KafkaConsumer>();
    })
    .Build();

await host.RunAsync();

