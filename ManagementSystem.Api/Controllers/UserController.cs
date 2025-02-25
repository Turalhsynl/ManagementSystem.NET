using Application.CQRS.Users.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    [Route("GetByEmail")]
    public async Task<IActionResult> GetByEmail([FromQuery] Application.CQRS.Users.Handlers.GetById.Query request)
    {
        return Ok(await _sender.Send(request));
    }


    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetById([FromQuery]Application.CQRS.Users.Handlers.GetById.Query request)
    {
        return Ok(await _sender.Send(request));
    }


    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] Application.CQRS.Users.Handlers.Register.Command request)
    {
        return Ok(await _sender.Send(request));
    }


    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] Application.CQRS.Users.Handlers.Update.Command request)
    {
        return Ok(await _sender.Send(request));
    }
}