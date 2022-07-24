using System;
using Confluent.Kafka;

namespace KafkaProducerConsumer
{
    public class KafkaProducer : IHostedService
    {
        private readonly ILogger<KafkaProducer> _logger;
        private IProducer<Null, string> _producer;

        public KafkaProducer(ILogger<KafkaProducer> logger)
        {
            _logger = logger;
            var configuration = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };

            _producer = new ProducerBuilder<Null, string>(configuration).Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            for(int i = 0; i < 100; i++)
            {
                var value = $"Hello World {i}";
                _logger.LogInformation(value);
                await _producer.ProduceAsync("topicDemo", new Message<Null, string>
                {
                    Value = value
                }, cancellationToken);
            }

            _producer.Flush(TimeSpan.FromSeconds(10));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

