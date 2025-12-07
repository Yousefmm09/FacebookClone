using FacebookClone.Core.Feature.Authentication.Command.Models;
using FacebookClone.Core.Feature.Authentication.Queries.Models;
using FacebookClone.Core.Feature.Users.Command.Models;
using FacebookClone.Core.Feature.Users.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserRegisterModel command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Contains("success", StringComparison.OrdinalIgnoreCase)
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] ResestPasswordCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Contains("success", StringComparison.OrdinalIgnoreCase)
                ? Ok(result)
                : BadRequest(result);
        }
    }
}
