using FacebookClone.Core.Feature.Admin.Command.Models;
using FacebookClone.Core.Feature.Admin.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FacebookClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var query = new GetAllUserQuery
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("banned")]
        public async Task<IActionResult> BannedUser([FromBody] BannedUserModel command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("unbanned")]
        public async Task<IActionResult> UnBannedUser([FromBody] UnbannedUserModel command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("details")]
        public async Task<IActionResult> GetUserDetails([FromForm] GetUserDetailsQuery query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
