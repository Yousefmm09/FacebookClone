using FacebookClone.Core.Feature.Comments.Command.Models;
using FacebookClone.Core.Feature.Comments.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FacebookClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateComment([FromBody] CreatCommentCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("single")]
        public async Task<IActionResult> GetComment([FromQuery] GetCommentByIdModel query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("post")]
        public async Task<IActionResult> GetPostComments([FromQuery] GetPostCommentsModel query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveComment([FromQuery] RemoveCommentCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditComment([FromQuery] EditCommentCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
