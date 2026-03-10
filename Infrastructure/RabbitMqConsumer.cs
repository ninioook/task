using Core.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

public class RabbitMqConsumer
{
    private readonly ConnectionFactory _factory;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMqConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _factory = new ConnectionFactory() { HostName = "localhost" };
    }

    public async Task StartConsuming()
    {
        var connection = await _factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "applications_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            using var scope = _serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IApplicationRepository>();

            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var application = JsonSerializer.Deserialize<Application>(json);

            if (application != null)
            {
                try
                {
                    await repository.Add(application, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    // Log the error
                    Console.WriteLine($"Failed to save application: {ex}");
                }
            }

            await channel.BasicAckAsync(ea.DeliveryTag, false);
        };

        await channel.BasicConsumeAsync(
            queue: "applications_queue",
            autoAck: false,
            consumer: consumer
        );
    }
}