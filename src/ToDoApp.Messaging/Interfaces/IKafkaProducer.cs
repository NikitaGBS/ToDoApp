namespace ToDoApp.Messaging.Interfaces;

public interface IKafkaProducer
{
    Task ProduceAsync<T>(string topic, T message);
}