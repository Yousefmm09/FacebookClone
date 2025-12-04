using FacebookClone.Core.Feature.Comments.Command.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacebookClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =("User"))]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommentController(IMediator mediator)
        {
         _mediator=mediator;   
        }
        [HttpPost("creatComment")]
        public async Task<IActionResult> CreatComment(CreatCommentCommand command)
        {
            if(ModelState.IsValid)
            {
                var res=  await _mediator.Send(command);
                return Ok(res);
            }
            return BadRequest(ModelState);
        }

    }
}
