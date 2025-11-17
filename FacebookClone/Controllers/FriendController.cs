using FacebookClone.Core.Feature.Friends.Command.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacebookClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class FriendController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FriendController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendRequest(SentFriendRequestCommand command)
        {
            if(ModelState.IsValid)
            {
               var res= await _mediator.Send(command);
                return Ok(res);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Accept")]
        public async Task<IActionResult> AcceptFriend(AcceptFriendCommand command)
        {
            if (ModelState.IsValid)
            {
                var res = await _mediator.Send(command);
                return Ok(res);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("Remove")]
        public async Task<IActionResult> RemoveFriend(RemoveFriendCommand command)
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
