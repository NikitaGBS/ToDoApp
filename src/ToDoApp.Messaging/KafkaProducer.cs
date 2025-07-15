using System.Text.Json;
using Confluent.Kafka;
using ToDoApp.Messaging.Interfaces;

namespace ToDoApp.Messaging;

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<Null, string> _producer;

    public KafkaProducer(string bootstrapServers)
    {
        var config = new ProducerConfig { BootstrapServers = bootstrapServers, Acks = Acks.Leader};
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task ProduceAsync<T>(string topic, T message)
    {
        var json = JsonSerializer.Serialize(message);
        await _producer.ProduceAsync(topic, new Message<Null, string> { Value = json });
    }
}