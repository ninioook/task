using Domain.Entities;

namespace Core.Interfaces;

public interface IRabbitPublisher
{
    Task PublishApplication(Application application);
}