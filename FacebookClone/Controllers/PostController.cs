using FacebookClone.Core.Feature.Post.Command.Handlers;
using FacebookClone.Core.Feature.Post.Command.Models;
using FacebookClone.Core.Feature.Post.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FacebookClone.Service.Dto;

[Route("api/posts")]
[ApiController]
[Authorize(Roles = "User")]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatPostCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] GetPostsPagedQuery query)
        => Ok(await _mediator.Send(query));

    [HttpGet("cursor")]
    public async Task<IActionResult> GetCursor([FromQuery] GetPostsCursorQuery query)
        => Ok(await _mediator.Send(query));

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchPostsQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetPostByIdQuery { PostId = id };
        return Ok(await _mediator.Send(query));
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePostCommand command)
        => Ok(await _mediator.Send(command));

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] DeletePostCommand command)
        => Ok(await _mediator.Send(command));

    [HttpPost("share")]
    public async Task<IActionResult> Share([FromBody] SharePostCommand command)
        => Ok(await _mediator.Send(command));
}
