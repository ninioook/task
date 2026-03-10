using Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class ApplicationConsumerService : BackgroundService
{
    private readonly RabbitMqConsumer _consumer;
    private readonly ILogger<ApplicationConsumerService> _logger;

    public ApplicationConsumerService(RabbitMqConsumer consumer, ILogger<ApplicationConsumerService> logger)
    {
        _consumer = consumer;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _consumer.StartConsuming();
        }
        catch (OperationCanceledException)
        {
            // shutting down
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Application consumer service failed");
        }

        // keep the background service alive until shutdown
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}
