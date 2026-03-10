using AutoMapper;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers;

public class CustomerController : Controller
{
    private readonly IMapper _mapper;
    private readonly RegisterCustommerCommandHandler _registerCustomerCommandHandler;

    public CustomerController(IMapper mapper,RegisterCustommerCommandHandler registerCustomerCommandHandler)
    {
            _mapper = mapper;
        _registerCustomerCommandHandler = registerCustomerCommandHandler;
    }
    public async Task<IActionResult >Register(CustomerModel model,CancellationToken cancellationToken)
    {
        var command = _mapper.Map<RegisterCommand>(model);
        await _registerCustomerCommandHandler.Handle(command, cancellationToken);

        return Ok();
    }
}