// Infrastructure/Messaging/RabbitMqPublisher.cs

using System.Text;
using System.Text.Json;
using Core;
using RabbitMQ.Client;

namespace Infrastructure;

public class RabbitMqPublisher:IRabbitPublisher
{
    private readonly ConnectionFactory _factory;

    public RabbitMqPublisher()
    {
        _factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };
    }

    public async void PublishApplication(object application)
    {
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "applications_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var message = JsonSerializer.Serialize(application);

        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: "applications_queue",
            body: body
        );
    }
}