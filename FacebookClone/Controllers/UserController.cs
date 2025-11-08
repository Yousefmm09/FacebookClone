<<<<<<< HEAD
﻿using FacebookClone.Core.Feature.Authentication.Command.Handlers;
using FacebookClone.Core.Feature.Authentication.Command.Models;
using FacebookClone.Core.Feature.Authentication.Queries.Models;

=======
>>>>>>> 5cf699279a9852b89884195a88d9491d456f3145
﻿using FacebookClone.Core.Feature.Authentication.Queries.Models;
using FacebookClone.Core.Feature.Users.Command.Handlers;
using FacebookClone.Core.Feature.Users.Command.Models;
using FacebookClone.Core.Feature.Users.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery command)
        {
            if (ModelState.IsValid)
            {
                var res = await _mediator.Send(command);
                return Ok(res);
            }
            return BadRequest(ModelState);
        }
<<<<<<< HEAD
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordCommand model)
        {
            var result = await _mediator.Send(model);
            return result.Contains("success", StringComparison.OrdinalIgnoreCase)
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpGet("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] ResestPasswordCommand model)
        {
            var result = await _mediator.Send(model);
            return result.Contains("success", StringComparison.OrdinalIgnoreCase)
                ? Ok(result)
                : BadRequest(result);
        }
=======
>>>>>>> 5cf699279a9852b89884195a88d9491d456f3145
    }
}
