using FacebookClone.Core.Feature.Post.Queries.Models;
using FacebookClone.Core.Feature.Posts.Command.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FacebookClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreatePost([FromBody] CreatPostCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePost([FromQuery] DeletePostCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("GetPostById")]
        public async Task<IActionResult> GetPostById([FromQuery] GetPosByIdQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
