using Confluent.Kafka;
using ToDoApp.Messaging.Interfaces;

namespace ToDoApp.Messaging;

public class KafkaConsumer : IKafkaConsumer
{
    private readonly IConsumer<Ignore, string> _consumer;

    public KafkaConsumer(string bootstrapServers, string groupId)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = bootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true
        };
        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
    }

    public void Consume(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(cancellationToken);
                Console.WriteLine($"Message received: {result.Message.Value}");
            }
        }
        catch (OperationCanceledException)
        {
            _consumer.Close();
        }
    }

    public void Subscribe(string topic)
    {
        _consumer.Subscribe(topic);
        Console.WriteLine($"Listening to Kafka topic: {topic}");
    }
    
    public void Close()
    {
        _consumer.Close();
        Console.WriteLine("Closing consumer");
    }
}