using FacebookClone.Core.Feature.Comments.Command.Models;
using FacebookClone.Core.Feature.Comments.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacebookClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ("User"))]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("creatComment")]
        public async Task<IActionResult> CreatComment(CreatCommentCommand command)
        {
            if (ModelState.IsValid)
            {
                var res = await _mediator.Send(command);
                return Ok(res);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("getComment")]
        public async Task<IActionResult> GetComment([FromQuery] GetCommentByIdModel query)
        {
            if (ModelState.IsValid)
            {
                var res = await _mediator.Send(query);
                return Ok(res);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetCommentPostsById")]
        public async Task<IActionResult> GetCommentPostsById([FromQuery] GetPostCommentsModel query)
        {
            if (ModelState.IsValid)
            {
                var res = await _mediator.Send(query);
                return Ok(res);
            }
            return BadRequest(ModelState);
        }
    }

}
