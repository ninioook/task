using AutoMapper;
using Core.Interfaces;
using Domain.Entities;

namespace Core.CommandHandlers;

public class RegisterCustommerCommandHandler
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;

    public RegisterCustommerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    public async Task Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        string passwordHash = PasswordHelper.HashPassword(command.Password);
        var customer = _mapper.Map<Customer>(command);
        customer.Password = passwordHash;
        customer.StatusId = (int)CustomerStatus.Active;
        await _customerRepository.Register(customer, cancellationToken);
    }
}