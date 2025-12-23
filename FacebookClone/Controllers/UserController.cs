using FacebookClone.Core.Feature.Authentication.Command.Models;
using FacebookClone.Core.Feature.Authentication.Queries.Models;
using FacebookClone.Core.Feature.Users.Command.Models;
using FacebookClone.Core.Feature.Users.Queries.Models;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FacebookClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;
        private readonly AppDb _context;

        public UserController(IMediator mediator, UserManager<User> userManager, AppDb context)
        {
            _mediator = mediator;
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("me")]
        //get user details
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirst("sub")?.Value ?? User.FindFirst("nameid")?.Value ?? User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized("User id not found in token.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return NotFound();

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.ProfilePictureUrl,
                user.Bio,
                user.CreatedAt
            });
        }
//search
        [Authorize]
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string q = "", [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(u =>
                    u.UserName.Contains(q) ||
                    u.Email.Contains(q));
            }

            var skip = (pageNumber - 1) * pageSize;
            var users = query
                .OrderBy(u => u.UserName)
                .Skip(skip)
                .Take(pageSize)
                .Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.Email,
                    u.ProfilePictureUrl
                })
                .ToList();

            return Ok(users);
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
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserRegisterModel command)
        {
            var result = await _mediator.Send(command);
            return result.Contains("success", StringComparison.OrdinalIgnoreCase)
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(string userId, string code)
        {
            var result = await _mediator.Send(
                new VerifyOtpEmailCommand(userId, code)
            );

            return result.Contains("success", StringComparison.OrdinalIgnoreCase)
                         ? Ok(result)
                         : BadRequest(result);
        }
        [ApiController]
        [Route("api/otp")]
        public class OtpController : ControllerBase
        {
            private readonly IMediator _mediator;

            public OtpController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpPost("send")]
            public async Task<IActionResult> SendOtp(string userId, string email)
            {
                var result = await _mediator.Send(
                    new CreateOtpEmailCommand(userId, email)
                );

                return result.Contains("success", StringComparison.OrdinalIgnoreCase)
                        ? Ok(result)
                        : BadRequest(result);
            }

            [HttpPost("verify")]
            public async Task<IActionResult> VerifyOtp(string userId, string code)
            {
                var result = await _mediator.Send(
                    new VerifyOtpEmailCommand(userId, code)
                );

                return result.Contains("success", StringComparison.OrdinalIgnoreCase)
                         ? Ok(result)
                         : BadRequest(result);
            }
        }


    }
}
