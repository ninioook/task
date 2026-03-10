using AutoMapper;
using Core;
using Core.CommandHandlers;
using Core.Interfaces;
using Core.QueryHandlers;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ApplicationCommandHandler _applicationCommandHandler;
    private readonly ApplicationQueryHandler _applicationQueryHandler;

    public ApplicationController(IMapper mapper, ApplicationCommandHandler applicationCommandHandler, ApplicationQueryHandler applicationQueryHandler)
    {
        _mapper = mapper;
        _applicationCommandHandler = applicationCommandHandler;
        _applicationQueryHandler= applicationQueryHandler;
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(
        [FromBody] ApplicationModel model,
        CancellationToken cancellationToken)
    {
        if (model == null)
            return BadRequest();

        var command = _mapper.Map<AddApplicationCommand>(model);

        await _applicationCommandHandler.Handle(command, cancellationToken);

        return Ok();
    }

    [HttpPut("updatestatus")]
    public async Task<IActionResult> UpdateStatus(long id, int statusId, CancellationToken cancellationToken)
    {
        var command=new UpdateApplicationStatusCommand
        {
            Id = id,
            StatusId = statusId
        };
        await _applicationCommandHandler.HandleStatusUpdate(command, cancellationToken);

        return Ok(new { Message = "Status updated successfully." });
    }

    [HttpGet("bystatus/{statusId}")]
    public async Task<IActionResult> GetByStatus(int statusId, CancellationToken cancellationToken)
    {
        if (statusId <= 0)
            return BadRequest("Invalid StatusId.");
        var query=new GetApplicationsByStatusQuery
        {
            StatusId = statusId
        };
        var applications = await _applicationQueryHandler.Handle(query, cancellationToken);

        return Ok(applications);
    }
}
