namespace ToDoApp.Messaging.Interfaces;

public interface IKafkaConsumer
{
    void Consume(CancellationToken cancellationToken);
    void Subscribe(string topic);
    void Close();
}