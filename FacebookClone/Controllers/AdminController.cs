using FacebookClone.Core.Feature.Admin.Command.Handlers;
using FacebookClone.Core.Feature.Admin.Command.Models;
using FacebookClone.Core.Feature.Admin.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public Task<IActionResult> GetAllUsers([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var query = new GetAllUserQuery
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            return _mediator.Send(query).ContinueWith<IActionResult>(t => Ok(t.Result));
        }
        [HttpPost("banned")]
        public async Task<IActionResult> BannedUser([FromBody] BannedUserModel command )
        {
         if(ModelState.IsValid)
            {
                var res= await _mediator.Send(command);
                return Ok(res);
            }
         return BadRequest(ModelState);
        }
        [HttpPost("Unbanned_User")]
        public async Task<IActionResult> UnBannedUser([FromBody] UnbannedUserModel command )
        {
         if(ModelState.IsValid)
            {
                var res= await _mediator.Send(command);
                return Ok(res);
            }
         return BadRequest(ModelState);
        }
        [HttpPost("UserDetails")]
        public async Task<IActionResult> getUserDetails([FromForm] GetUserDetailsQuery query )
        {
         if(ModelState.IsValid)
            {
                var res= await _mediator.Send(query);
                return Ok(res);
            }
         return BadRequest(ModelState);
        }
    }
}
