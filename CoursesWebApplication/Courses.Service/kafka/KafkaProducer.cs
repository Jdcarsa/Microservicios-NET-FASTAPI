using Confluent.Kafka;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Courses.Service.kafka
{
    public class KafkaProducer
    {
        private readonly IProducer<string, string> _producer;
        private const string DefaultTopic = "course_events";

        public KafkaProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                Acks = Acks.All,
                EnableIdempotence = true,
                MessageSendMaxRetries = 3,
                RetryBackoffMs = 100
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task SendMessage<T>(string eventType, T payload, string? topic = null, string? key = null)
        {
            var payloadDict = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(payload));
            payloadDict["EventType"] = eventType;
            payloadDict["Timestamp"] = DateTime.UtcNow;

            var messageValue = JsonSerializer.Serialize(payloadDict);

            var message = new Message<string, string>
            {
                Key = key ?? Guid.NewGuid().ToString(),
                Value = messageValue
            };

            await _producer.ProduceAsync(topic ?? DefaultTopic, message);
        }
    }
}
