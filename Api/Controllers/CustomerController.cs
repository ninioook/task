using AutoMapper;
using Core;
using Core.CommandHandlers;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly RegisterCustommerCommandHandler _registerCustomerCommandHandler;

    public CustomerController(IMapper mapper, RegisterCustommerCommandHandler registerCustomerCommandHandler)
    {
        _mapper = mapper;
        _registerCustomerCommandHandler = registerCustomerCommandHandler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CustomerModel model, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<RegisterCommand>(model);
        await _registerCustomerCommandHandler.Handle(command, cancellationToken);

        return Ok();
    }
}