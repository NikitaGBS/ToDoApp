using ToDoApp.Messaging;

Console.WriteLine("ToDoApp Consumer started. Press Ctrl+C to stop.");

var cancellationTokenSource = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cancellationTokenSource.Cancel();
};

var consumer = new KafkaConsumer("localhost:9092", "todoapp-consumer");
consumer.Subscribe(KafkaTopics.TodoCreated);

try
{
    while (!cancellationTokenSource.Token.IsCancellationRequested)
    {
        consumer.Consume(cancellationTokenSource.Token);
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("Consumer stopped by user.");
}
finally
{
    consumer.Close();
}