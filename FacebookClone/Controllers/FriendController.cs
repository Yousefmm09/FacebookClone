using FacebookClone.Core.Feature.Friends.Command.Models;
using FacebookClone.Core.Feature.Friends.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FacebookClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class FriendController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FriendController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendFriendRequest([FromBody] SentFriendRequestCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptFriend([FromBody] AcceptFriendCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFriend([FromQuery] RemoveFriendCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchFriends([FromQuery] string searchTerm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var query = new SearchFriendsQuery(searchTerm, userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("my-friends")]
        public async Task<IActionResult> GetMyFriends()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var query = new GetFriendsListQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("pending-requests")]
        public async Task<IActionResult> GetPendingFriendRequests()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var query = new GetPendingFriendRequestsQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
