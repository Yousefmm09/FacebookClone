using FacebookClone.Core.Feature.Like.Command.Models;
using FacebookClone.Core.Feature.Like.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacebookClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class LikeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LikeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("setLike")]
        public async Task<IActionResult> Setlike([FromBody] SetLikeCommand command)
        {
            if(ModelState.IsValid)
            {
               var res= await _mediator.Send(command);
                return Ok( res);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("getLikesCount")]
        public async Task<IActionResult> GetLikesCount([FromQuery] GetCountLikesQuery command)
        {
            if (ModelState.IsValid)
            {
                var res = await _mediator.Send(command);
                return Ok(res);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("removeLike")]
        public async Task<IActionResult> RemoveLike([FromQuery] RemoveLikeCommand command)
        {
            if (ModelState.IsValid)
            {
                var res = await _mediator.Send(command);
                return Ok(res);
            }
            return BadRequest(ModelState);
        }
    }
}
