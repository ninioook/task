namespace Core;

public interface IRabbitPublisher
{
    void PublishApplication(object application);
}