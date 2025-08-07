using Application.Model.userModel.model;
using Confluent.Kafka;
using System.Text.Json;

namespace User.Service.kafka
{
    public class KafkaProducer
    {
        private readonly IProducer<string, string> _producer;
        private readonly string _topic;

        public KafkaProducer(IConfiguration config)
        {
            var kafkaConfig = new ProducerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"]
            };

            _producer = new ProducerBuilder<string, string>(kafkaConfig).Build();
            _topic = config["Kafka:Topic"];
        }

        public async Task SendUserCreatedEvent(UserModel user)
        {
            var payload = JsonSerializer.Serialize(new
            {
                user.Id,
                user.FullName,
                user.Email,
                user.PhoneNumber
            });

            var message = new Message<string, string>
            {
                Key = user.Id.ToString(),
                Value = payload
            };

            await _producer.ProduceAsync(_topic, message);
        }
    }
}
