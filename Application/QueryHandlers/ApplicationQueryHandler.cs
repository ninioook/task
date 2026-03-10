using Core.Interfaces;
using Domain.Entities;

namespace Core.QueryHandlers
{
    public class ApplicationQueryHandler
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationQueryHandler(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<IEnumerable<Application>> Handle(GetApplicationsByStatusQuery query, CancellationToken cancellationToken)
        {
            return await _applicationRepository.GetByStatusId(query.StatusId, cancellationToken);
        }
    }
}
