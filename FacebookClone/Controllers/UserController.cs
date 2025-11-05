using FacebookClone.Core.Feature.Users.Command.Handlers;
using FacebookClone.Core.Feature.Users.Command.Models;
using FacebookClone.Core.Feature.Users.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacebookClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterModel command)
        {
            if(ModelState.IsValid)
            {
                var res = await _mediator.Send(command);
                return Ok(res);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]UserLoginModel command)
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
