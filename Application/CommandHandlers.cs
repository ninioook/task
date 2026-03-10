using AutoMapper;

namespace Core;

public class RegisterCustommerCommandHandler
{
    private readonly IMapper _mapper;
    private readonly IRabbitPublisher _publisher;
    private readonly ICustomerRepository _customerRepository;

    public RegisterCustommerCommandHandler(ICustomerRepository customerRepository,IRabbitPublisher publisher,IMapper mapper)
    {
        _mapper = mapper;
        _publisher = publisher;
        _customerRepository = customerRepository;
    }

    public async Task Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Customer>(command);
        await _customerRepository.Register(customer,cancellationToken);
    }
}