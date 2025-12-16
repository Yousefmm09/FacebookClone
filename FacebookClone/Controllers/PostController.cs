using FacebookClone.Core.Feature.Post.Command.Models;
using FacebookClone.Core.Feature.Post.Queries.Models;
using FacebookClone.Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FacebookClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AppDb _context;

        public PostController(IMediator mediator, AppDb context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatPostCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var skip = (pageNumber - 1) * pageSize;
            var posts = await _context.Posts
                .Include(p => p.user)
                .OrderByDescending(p => p.CreatedAt)
                .Skip(skip)
                .Take(pageSize)
                .Select(p => new
                {
                    p.Id,
                    p.Content,
                    p.Privacy,
                    p.CreatedAt,
                    p.UpdatedAt,
                    Author = new { p.user.Id, p.user.UserName, p.user.ProfilePictureUrl },
                    p.LikeCount,
                    p.CommentCount,
                    p.ShareCount
                })
                .ToListAsync();

            return Ok(posts);
        }
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string post = "", [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var query = _context.Posts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(post))
            {
                 query=query.Where(x => x.Content.Contains(post));
            }
            var skip = (pageNumber - 1) * pageSize; // 
            var search = query.
                OrderBy(x => x.CreatedAt)
                .Skip(skip)
                .Take(pageSize)
                .Select(x=> new
                {
                    x.Content,
                    x.CommentCount,
                    x.LikeCount,
                })
                .ToList();
            return Ok(search);
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePost([FromQuery] DeletePostCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById([FromRoute] int id)
        {
            var query = new GetPosByIdQuery { PostId = id };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("share")]
        public async Task<IActionResult> SharePost([FromBody] SharePostCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
