using Domain.Entities;

namespace Core.Interfaces
{
    public interface IApplicationRepository
    {

        Task Add(Application application, CancellationToken cancellationToken);
        Task UpdateStatus(long applicationId, int statusId, CancellationToken cancellationToken);
        Task<IEnumerable<Application>> GetByStatusId(int statusId, CancellationToken cancellationToken);
    }
}