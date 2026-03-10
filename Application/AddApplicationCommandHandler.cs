using AutoMapper;

namespace Core;

public class AddApplicationCommandHandler
{
    private readonly IMapper _mapper;
    private readonly IRabbitPublisher _publisher;

    public AddApplicationCommandHandler(IRabbitPublisher publisher,IMapper mapper)
    {
        _mapper = mapper;
        _publisher = publisher;
    }
    
    
    public  async Task Handle(AddApplicationCommand command, CancellationToken cancellationToken)
    {
        var app = _mapper.Map<Application>(command);
        
         _publisher.PublishApplication(app);
    }
}