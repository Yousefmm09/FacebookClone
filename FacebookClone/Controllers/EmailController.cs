using FacebookClone.Core.Feature.Email.Command.Handlers;
using FacebookClone.Core.Feature.Email.Command.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacebookClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmailController(IMediator mediator)
        {
            _mediator=mediator;
        }
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromForm] SentEmaiModel command)
        {
            if(ModelState.IsValid)
            {
                var res = await _mediator.Send(command);
                return Ok(res);
            }
            return BadRequest(ModelState);
        }
    }
}
