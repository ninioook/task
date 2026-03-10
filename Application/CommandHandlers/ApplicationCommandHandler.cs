using AutoMapper;
using Core.Interfaces;
using Domain.Entities;

namespace Core.CommandHandlers;

public class ApplicationCommandHandler
{
    private readonly IMapper _mapper;
    private readonly IRabbitPublisher _publisher;
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationCommandHandler(IRabbitPublisher publisher, IMapper mapper, IApplicationRepository applicationRepository)
    {
        _mapper = mapper;
        _publisher = publisher;
        _applicationRepository = applicationRepository;
    }

    public async Task Handle(AddApplicationCommand command, CancellationToken cancellationToken)
    {
        var app = _mapper.Map<Application>(command);
        app.StatusId = (int)ApplicationStatus.InProgress;

        await _publisher.PublishApplication(app);
    }

    public async Task HandleStatusUpdate(UpdateApplicationStatusCommand command, CancellationToken cancellationToken)
    {
        await _applicationRepository.UpdateStatus(command.Id, command.StatusId, cancellationToken);
    }
}